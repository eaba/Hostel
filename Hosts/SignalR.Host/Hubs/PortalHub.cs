using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace SignalR.Host.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PortalHub : Hub
    {
        private readonly string _connectString;
        private IBusControl _bus;
        public PortalHub(IBusControl bus, IConfiguration configuration)
        {
            _bus = bus;
            _connectString = configuration.GetConnectionString("OutBox");
        }
        public override async Task OnConnectedAsync()
        {
            var username = Context.User.Identity.Name;
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
            //var msgs = await new Db.Notification(_connectString).GetNotifications(username, "PortalHub");
            var clientProxy = Clients.Group(username);
            /*foreach (var n in nots)
            {
                var method = n["Method"].ToLower().Trim();
                var dto = n["Dto"];
                await clientProxy.SendAsync(method, dto);
            }*/
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            await base.OnDisconnectedAsync(exception);
        }
        public async Task Delete(string key)
        {
            try
            {
                //await new Db.Notification(_connectString).DeleteNotifications(key);
            }
            catch (Exception ex)
            {
               // Log.LogError(ex.ToString(), "DelError");
            }
            await Task.CompletedTask;
        }
    }
}
