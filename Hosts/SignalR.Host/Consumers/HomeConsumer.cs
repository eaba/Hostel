using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Shared;
using SignalR.Host.Hubs;
using System;
using System.Threading.Tasks;

namespace SignalR.Host.Consumers
{
    public class HomeConsumer : IConsumer<IMassTransitEvent>
    {
        private readonly IHubContext<HomeHub> _hub;
        public HomeConsumer(IHubContext<HomeHub> hub)
        {
            _hub = hub ?? throw new ArgumentNullException(nameof(hub)); ;
        }
        public async Task Consume(ConsumeContext<IMassTransitEvent> context)
        {
            var message = context.Message;
            var @event = new { message.Success, message.Error, Id = message.CommandId, message.Payload};
            await _hub.Clients.Group(message.Commander).SendAsync(message.Event.ToLower(), JsonConvert.SerializeObject(@event, Formatting.Indented));
        }
    }
}
