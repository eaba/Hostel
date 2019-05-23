using Akka.Actor;
using Akka.Extension;
using IdentityServer.Host.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using MassTransit.Command;
using MassTransit;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using Shared;
using MassTransit.Event;

namespace IdentityServer.Host.Actors
{
    public class ProcessorActor:ReceiveActor
    {
        private UserManager<HostelUser> _userManager;
        private ISendEndpoint _sendEndPoint;
        private Uri _queue => new Uri("rabbitmq://localhost/hostel_home_queue");
        public ProcessorActor()
        {
            ReceiveAsync<CreateAccount>(CreateAccount);
        }
        protected override void PreStart()
        {
            using (var context = Context.CreateScope())
            {
                _userManager = context.ServiceProvider.GetRequiredService<UserManager<HostelUser>>();
                var bus = context.ServiceProvider.GetService<IBusControl>();
                _sendEndPoint = bus.GetSendEndpoint(_queue).GetAwaiter().GetResult();
            }
            base.PreStart();
        }
        public static Props Prop()
        {
            return Props.Create(() => new ProcessorActor());
        }
        private async Task CreateAccount(CreateAccount command)
        {
            var payload = command.Payload;
            var response = new Dictionary<string, string>();
            try
            {
                var user = new HostelUser
                {
                    UserName = payload["Email"],
                    Email = payload["Email"],
                    PhoneNumber = payload["Phone"],
                    Hostel = "Baafog"
                };
                var created = await _userManager.CreateAsync(user, payload["Password"]);
                if (created.Succeeded)
                {
                    var userClaims = new List<Claim>();
                    var userRole = payload["Role"];
                    userClaims.Add(new Claim(JwtClaimTypes.Role, userRole));
                    userClaims.AddRange(new Claim[]{
                            new Claim("PreferredUserName", payload["Email"]),
                            new Claim("Role", userRole)
                        });
                    var claim = await _userManager.AddClaimsAsync(user, userClaims);
                    if (!claim.Succeeded)
                    {
                        await _userManager.DeleteAsync(user);
                        response["Created"] = "false";
                        response["Type"] = "AddClaimsAsync";
                        response["Errors"] = string.Join(", ", claim.Errors);
                        response["Message"] = "Your account created was aborted";
                    }
                }
                else
                {
                    response["Created"] = "false";
                    response["Type"] = "CreateAsync";
                    response["Errors"] = string.Join(", ", created.Errors);
                    response["Message"] = "Your account was not created";
                }                
            }
            catch(Exception e)
            {
                response["Message"] = e.Message;
            }
            var @event = new MassTransitEvent("AccountCreated", command.Commander, command.CommandId, response);
            await SendToQueue(@event);
            await Self.GracefulStop(TimeSpan.FromSeconds(10));
        }
        private async Task SendToQueue(IMassTransitEvent evnt)
        {
            if (_sendEndPoint != null)
            {
                await _sendEndPoint.Send(evnt);
            }
            else
            {
                using (var context = Context.CreateScope())
                {
                    var bus = context.ServiceProvider.GetService<IBusControl>();
                    _sendEndPoint = await bus.GetSendEndpoint(_queue);
                    await _sendEndPoint.Send(evnt);
                }
            }
            
        }
    }
}
