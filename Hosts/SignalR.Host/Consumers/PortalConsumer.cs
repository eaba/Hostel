using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Shared;
using SignalR.Host.Hubs;
using System;
using System.Threading.Tasks;

namespace SignalR.Host.Consumers
{
    public class PortalConsumer : IConsumer<IMassTransitEvent>
    {
        private readonly IHubContext<PortalHub> _hub;
        private readonly string _connectString;
        public PortalConsumer(IHubContext<PortalHub> hub, IConfiguration configuration)
        {
            _hub = hub ?? throw new ArgumentNullException(nameof(hub));
            _connectString = configuration["OutBox"];
        }
        public async Task Consume(ConsumeContext<IMassTransitEvent> context)
        {
            var message = context.Message;
            await _hub.Clients.Group(message.Commander).SendAsync(message.Event, message.Payload, message.CommandId);
        }
    }
}
