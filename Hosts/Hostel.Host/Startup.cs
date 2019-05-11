using Akka.Actor;
using Akka.Extension;
using GreenPipes;
using Hostel.Entity;
using Hostel.Entity.Handler;
using Hostel.State;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Hostel.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
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
                    cfg.ReceiveEndpoint(host, "hostel_queue", e =>
                    {
                        e.PrefetchCount = 500;
                        e.UseRetry(r => r.Immediate(5));
                        e.Consumer<CommandConsumer>(provider);
                    });                    
                }));
            });
                
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            /* Register the ActorSystem*/
            services.AddSingleton(provider =>
            {
                var serviceScopeFactory = provider.GetService<IServiceScopeFactory>();
                var actorSystem = ActorSystem.Create("HostelActorSystem", ConfigurationLoader.Load("host.hocon"));
                actorSystem.AddServiceScopeFactory(serviceScopeFactory);
                return actorSystem;
            });

            services.AddSingleton(provider =>
            {
                var actorSystem = provider.GetService<ActorSystem>();
                var hostelManagerActor = actorSystem.ActorOf(HostelManagerActor.Prop(new HostelManagerHandler(), HostelManagerState.Empty, "HostelManager", Configuration.GetConnectionString("Database")), "HostelManager");
                return hostelManagerActor;
            });
            services.AddSingleton<IHostedService, HostelService>();
        }
        public IConfiguration Configuration { get; }
        public void Configure(IApplicationBuilder app)
        {
            //app.UseMvc();
            //app.ApplicationServices.GetService<AccountHostedService>().StartAsync(CancellationToken.None); //start MassTransit and Akka.NET
        }
    }
}
