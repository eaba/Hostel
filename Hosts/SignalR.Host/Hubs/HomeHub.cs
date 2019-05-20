using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalR.Host.Hubs
{
    public class HomeHub:Hub
    {
        private IBusControl _bus;
        private string _commander;
        public HomeHub(IBusControl bus)
        {
            _bus = bus;
        }
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            _commander = httpContext.Request.Query["commander"];
            await Groups.AddToGroupAsync(Context.ConnectionId, _commander);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _commander);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
