using Akka.Actor;
using MassTransit;
using Shared;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Host.Consumers
{
    public class CommandConsumer : IConsumer<IMassTransitCommand>
    {
        private readonly IActorRef _actorRef;
        public CommandConsumer(IActorRef actor)
        {
            _actorRef = actor;
        }
        public Task Consume(ConsumeContext<IMassTransitCommand> context)
        {
            _
            throw new NotImplementedException();
        }
    }
}
