using Akka.Actor;
using Akka.Extension;
using GreenPipes;
using Hostel.Command;
using Hostel.Entity;
using Hostel.Entity.Handler;
using Hostel.Model;
using Hostel.State;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using static Hostel.Model.Construction;

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
                HostActorRef.ActorRef = hostelManagerActor;
                HostActorRef.ActorRef.Tell(Construct());
                HostActorRef.ActorIsReady = true;
                HostActorRef.ProcessCached();
                return hostelManagerActor;
            });
            services.AddSingleton<IHostedService, HostelService>();
        }

        private ConstructHostel Construct()
        {
            var construction = new Construction(new HostelDetail("Baafog", "Onikolobo Abeokuta, Ogun, Nigeria"))
                .WithFloor("Ground-Floor", "", 20, "", 2, "T", 2, "B", "K")
                .WithFloor("First-Floor", "1", 22, "1", 3, "T1", 2, "B", "K")
                .WithFloor("Second-Floor", "1", 23, "1", 3, "T1", 2, "B", "K")
                .WithSepticTank("Septic", 10, 7)
                .WithReservoir("Reservoir", 20, 15);
            return new ConstructHostel(construction);
        }

        public IConfiguration Configuration { get; }
        public void Configure(IApplicationBuilder app)
        {
            //app.UseMvc();
            //app.ApplicationServices.GetService<AccountHostedService>().StartAsync(CancellationToken.None); //start MassTransit and Akka.NET
        }
    }
}
