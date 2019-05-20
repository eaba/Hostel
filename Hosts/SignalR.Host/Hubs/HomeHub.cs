using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalR.Host.Hubs
{
    public class HomeHub:Hub
    {
        private IBusControl _bus;
        public HomeHub(IBusControl bus)
        {
            _bus = bus;
        }
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var commander = httpContext.Request.Query["commander"];
            await Groups.AddToGroupAsync(Context.ConnectionId, commander);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            var commander = httpContext.Request.Query["commander"];
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, commander);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
