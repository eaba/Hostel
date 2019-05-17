using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Shared;
using System;
using System.Threading.Tasks;

namespace SignalR.Host
{
    public class ClientConsumer : IConsumer<IMassTransitEvent>
    {
        private readonly IHubContext<HomeHub> _hub;
        public ClientConsumer(IHubContext<HomeHub> hub)
        {
            _hub = hub ?? throw new ArgumentNullException(nameof(hub)); ;
        }
        public async Task Consume(ConsumeContext<IMassTransitEvent> context)
        {
            var message = context.Message;
            await _hub.Clients.Group(message.Commander).SendAsync(message.Event, message.Payload);
        }
    }
}
