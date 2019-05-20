using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Shared;
using SignalR.Host.Hubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Host.Consumers
{
    public class PortalConsumer : IConsumer<IMassTransitEvent>
    {
        private readonly IHubContext<PortalHub> _hub;
        public PortalConsumer(IHubContext<PortalHub> hub)
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
