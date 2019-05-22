using MassTransit;
using IdentityServer4.Events;
using System.Threading.Tasks;

namespace IdentityServer.Host.Consumers
{
    public class IdentityServerEventsConsumer : IConsumer<Event>
    {
        public Task Consume(ConsumeContext<Event> context)
        {
            throw new System.NotImplementedException();
        }
    }
}
