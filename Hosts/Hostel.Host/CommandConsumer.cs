using Akka.Actor;
using MassTransit;
using Shared;
using System.Threading.Tasks;

namespace Hostel.Host
{
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
