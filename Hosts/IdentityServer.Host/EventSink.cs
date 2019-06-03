using Akka.Actor;
using IdentityServer4.Events;
using IdentityServer4.Services;
using System.Threading.Tasks;

namespace IdentityServer.Host
{
    public class EventSink: IEventSink
    {
        public EventSink()
        {
        }
        public async Task PersistAsync(Event evt)
        {
            IdentityActorStatic.Identity.Tell(evt);
            await Task.CompletedTask;
        }
    }
}
