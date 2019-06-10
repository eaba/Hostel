using System;
using System.Threading.Tasks;
using Hostel.Web.Landing.MVC;
using Hostel.Web.Portal.MVC.AutoRefresh;
using IdentityModel;
using MassTransit;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Hostel.Web.Portal.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            })
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie.Name = "hostelrefresh";
            })
            .AddAutomaticTokenRefresh()
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
             {
                 options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;
                 options.Authority = Configuration["Authority"];
                 options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 options.SignedOutRedirectUri = Configuration["Redirect"];
                 options.ClientId = "868daf6f-aff1-4b86-8811-89d9f671ed67";
                 options.ClientSecret = "c5b3f7d2-c60a-40a7-9675-8df465b77e0c";
                 options.ResponseType = OidcConstants.ResponseTypes.CodeIdToken;
                 options.GetClaimsFromUserInfoEndpoint = true;
                 options.SaveTokens = true;
                 options.RequireHttpsMetadata = false; //due to nginx ingress controller issues
                 options.UseTokenLifetime = true;
                 options.Scope.Clear();
                 options.Scope.Add("openid");
                 options.Scope.Add("profile");
                 options.Scope.Add("web");
                 options.Scope.Add("offline_access");
                 options.ClaimActions.MapAllExcept("iss", "nbf", "exp", "aud", "nonce", "iat", "c_hash");
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     NameClaimType = JwtClaimTypes.Name,
                     RoleClaimType = JwtClaimTypes.Role,
                 };
                 options.Events.OnAuthenticationFailed = faildMsg =>
                 {
                     if (faildMsg.Exception is OpenIdConnectProtocolInvalidNonceException)
                     {
                         if (faildMsg.Exception.Message.Contains("IDX10311"))
                         {
                             faildMsg.SkipHandler();
                         }
                     }
                     return Task.CompletedTask;
                 };
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("tenant", policyAdmin =>
                {
                    policyAdmin.RequireClaim("role", new[] { "tenant" });
                });
                options.AddPolicy("owner", policyUser =>
                {
                    policyUser.RequireClaim("role", new[] { "owner" });
                });
            });
            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost:/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            }));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins(Configuration["Redirect"]) //Note:  The URL must be specified without a trailing slash (/).
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, PortalService>();
            services.AddAntiforgery(options => {
                options.HeaderName = "X-CSRF-TOKEN";
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IAntiforgery antiforgery)
        {
            app.UseHsts();
            app.UseCors();
            app.UseAuthentication();
            app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
            app.Use(async (context, next) =>
            {
                var tokens = antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append("X-CSRF-TOKEN", tokens.RequestToken,
                    new CookieOptions() { HttpOnly = false });
                await next();
            });
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/";
                    await next();
                }
            });
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc(routes => {
                routes.MapRoute("Default", "{controller}/{action}/{id?}");
            }); //support attribute routing 
        }
    }
}
