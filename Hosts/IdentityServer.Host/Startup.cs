using Akka.Actor;
using Akka.Extension;
using GreenPipes;
using IdentityServer.Host.Actors;
using IdentityServer.Host.Consumers;
using IdentityServer.Host.Data;
using IdentityServer.Host.Services;
using IdentityServer4.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace IdentityServer.Host
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }


        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var dataProtectionProviderType = typeof(DataProtectorTokenProvider<HostelUser>);
            var phoneNumberProviderType = typeof(PhoneNumberTokenProvider<HostelUser>);
            var emailTokenProviderType = typeof(EmailTokenProvider<HostelUser>);

            var cert = new X509Certificate2(Path.Combine(Environment.ContentRootPath, "IdentityServer4.pfx"), "IdentityServer4");

            string connectionString = Configuration.GetConnectionString("Database");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<HostelUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider(TokenOptions.DefaultProvider, dataProtectionProviderType)
                .AddTokenProvider("email", emailTokenProviderType)
                .AddTokenProvider("sms", phoneNumberProviderType);

            services.AddMvc();
            services.AddSingleton<IProfileService, ProfileService>();
            services.AddSingleton<IEventSink, EventSink>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
            });

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
                iis.ForwardClientCertificate = true;
            });

            var builder = services.AddIdentityServer(options =>
            {
                options.PublicOrigin = "https://login.hostel.com";
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.Authentication.CookieSlidingExpiration = true;
                options.Authentication.RequireAuthenticatedUserForSignOutMessage = true;
                options.Authentication.CookieLifetime = TimeSpan.FromHours(24);
                options.Authentication.CheckSessionCookieName = "Hostel";
                options.Endpoints.EnableAuthorizeEndpoint = true;
                options.Endpoints.EnableCheckSessionEndpoint = true;
                options.Endpoints.EnableDiscoveryEndpoint = true;
                options.Endpoints.EnableIntrospectionEndpoint = true;
                options.Endpoints.EnableUserInfoEndpoint = true;
                options.IssuerUri = "https://login.hostel.com";
            })
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 60; // frequency in seconds to cleanup stale grants. 15 is useful during debugging
                }).AddSigningCredential(cert)
                .AddAspNetIdentity<HostelUser>()
                .AddProfileService<ProfileService>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<CommandConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://localhost:/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint(host, "hostel_identity_queue", e =>
                    {
                        e.PrefetchCount = 500;
                        e.UseRetry(r => r.Immediate(5));
                        e.Consumer<CommandConsumer>(provider);
                    });
                }));
            });
            services.AddSingleton(provider =>
            {
                var serviceScopeFactory = provider.GetService<IServiceScopeFactory>();
                var actorSystem = ActorSystem.Create("IdentityActorSystem", ConfigurationLoader.Load("host.hocon"));
                actorSystem.AddServiceScopeFactory(serviceScopeFactory);
                return actorSystem;
            });

            services.AddSingleton<IActorRef>(provider =>
            {
                var actorSystem = provider.GetService<ActorSystem>();
                var identityActor = actorSystem.ActorOf(IdentityManagerActor.Prop("IdentityServer4"), "IdentityServer4");
                return identityActor;
            });
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, IdentityService>();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRewriter(new RewriteOptions().AddRedirectToHttps());
            //app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}
