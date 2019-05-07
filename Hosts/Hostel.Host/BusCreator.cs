using MassTransit;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System.Threading.Tasks;
using Akka.Actor;
using GreenPipes;

namespace Hostel.Host
{
    public static class BusCreator
    {
        public static IBusControl BusControl;
        public static IServiceProvider ServiceProvider;
        public static IBusControl CreateBus(IConfiguration config, IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost:/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint(host, "hostel_queue", ep =>
                {
                    ep.PrefetchCount = 500;
                    ep.UseRetry(r => r.Immediate(5));
                    ep.Consumer<CommandConsumer>();
                });
            });
        }
        public class CommandConsumer : IConsumer<IMassTransitCommand>
        {
            public Task Consume(ConsumeContext<IMassTransitCommand> context)
            {
                var command = context.Message;
                if (HostActorRef.ActorIsReady)
                {
                    HostActorRef.ActorRef.Tell(command);
                }
                else
                {
                    HostActorRef.CacheDto(command);
                }
                return Task.CompletedTask;
            }
        }
    }
}
