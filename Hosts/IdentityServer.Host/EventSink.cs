using Akka.Actor;
using IdentityServer4.Events;
using IdentityServer4.Services;
using MassTransit;
using System.Threading.Tasks;

namespace IdentityServer.Host
{
    public class EventSink: IEventSink
    {
        private readonly IActorRef _actorRef;
        private ISendEndpoint _sendEndPoint;
        public EventSink(IActorRef actorRef)
        {
            _actorRef = actorRef;
        }
        public async Task PersistAsync(Event evt)
        {
            _actorRef.Tell(evt);
            await Task.CompletedTask;
        }
    }
}
