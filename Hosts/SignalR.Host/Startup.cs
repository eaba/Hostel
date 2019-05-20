using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using IdentityModel;
using GreenPipes;
using Microsoft.AspNetCore.Rewrite;
using SignalR.Host.Consumers;
using SignalR.Host.Hubs;
using System;

namespace SignalR.Host
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        private readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());
        private readonly JwtSecurityTokenHandler JwtTokenHandler = new JwtSecurityTokenHandler();
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins(new[] { "https://portal.hostel.com", "https://hostel.com" })
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddSignalR(options =>{ options.KeepAliveInterval = TimeSpan.FromSeconds(5); });
            var messagingPolicy = new AuthorizationPolicyBuilder().RequireClaim("Owner", "Tenant").Build();
            services.Configure<AuthorizationOptions>(options =>
            {
                options.AddPolicy("Web", messagingPolicy);
            });
            services.AddMassTransit(x =>
            {
                x.AddConsumer<HomeConsumer>();
                x.AddConsumer<PortalConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://localhost:/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint(host, "hostel_home_queue", e =>
                    {
                        e.PrefetchCount = 500;
                        e.UseRetry(r => r.Immediate(5));
                        e.Consumer<HomeConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint(host, "hostel_portal_queue", e =>
                    {
                        e.PrefetchCount = 500;
                        e.UseRetry(r => r.Immediate(5));
                        e.Consumer<PortalConsumer>(provider);
                    });
                }));
            });
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, ServiceManager>();
            services.AddMvc();
            ConfigureAuthService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
            app.UseCors("CorsPolicy");
            //app.UseAuthentication();
            app.UseSignalR(routes =>
            {
                routes.MapHub<HomeHub>("/home");
                routes.MapHub<PortalHub>("/portal");
            });
            //app.UseMvcWithDefaultRoute();
        }
        private void ConfigureAuthService(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.NameIdentifier);
                });
            });
            var guestPolicy = new AuthorizationPolicyBuilder()
            .RequireClaim("scope", "web")
            .Build();
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = Configuration["Authority"],
                ValidAudience = "web",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HostelSignalRValidatorParameter")),
                NameClaimType = JwtClaimTypes.PreferredUserName,
                RoleClaimType = JwtClaimTypes.Role,
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler
            {
                InboundClaimTypeMap = new Dictionary<string, string>()
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.Authority = Configuration["Authority"];
                options.RequireHttpsMetadata = false; //nginx ingress controller issues
                options.Audience = "web";
                options.IncludeErrorDetails = true;
                options.SaveToken = true;
                options.SecurityTokenValidators.Clear();
                options.SecurityTokenValidators.Add(jwtSecurityTokenHandler);
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        if (!string.IsNullOrEmpty(accessToken) && (context.HttpContext.WebSockets.IsWebSocketRequest || context.Request.Headers["Accept"] == "text/event-stream"))
                        {
                            context.Token = context.Request.Query["access_token"];
                        }
                        else if (context.HttpContext.WebSockets.IsWebSocketRequest && context.Request.Query.ContainsKey("Authorization"))
                        {
                            context.Token = context.Request.Query["Authorization"];
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        var te = context.Exception;
                        return Task.CompletedTask;
                    }

                };
            });
        }
    }
}
