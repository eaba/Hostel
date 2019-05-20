using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalR.Host.Hubs
{
    public class PortalHub : Hub
    {
        private IBusControl _bus;
        public PortalHub(IBusControl bus)
        {
            _bus = bus;
        }
        public override async Task OnConnectedAsync()
        {
            var clientProxy = Clients.Clients(Context.ConnectionId);
            await clientProxy.SendAsync("connected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
