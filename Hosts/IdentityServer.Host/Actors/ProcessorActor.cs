using Akka.Actor;
using Akka.Extension;
using IdentityServer.Host.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using MassTransit;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using Shared;
using MassTransit.Event;
using IdentityServer.Host.Commands;

namespace IdentityServer.Host.Actors
{
    public class ProcessorActor:ReceiveActor
    {
        private ISendEndpoint _homeSendEndPoint;
        private ISendEndpoint _portalSendEndPoint;
        private Uri _queue => new Uri("rabbitmq://localhost/hostel_home_queue");
        private Uri _queuePortal => new Uri("rabbitmq://localhost/hostel_portal_queue");
        public ProcessorActor()
        {
            ReceiveAsync<CreateAccount>(CreateAccount);
            ReceiveAsync<AddHostelClaim>(AddHostelClaim);
        }
        protected override void PreStart()
        {
            using (var context = Context.CreateScope())
            {
                var bus = context.ServiceProvider.GetService<IBusControl>();
                _homeSendEndPoint = bus.GetSendEndpoint(_queue).GetAwaiter().GetResult();
                _portalSendEndPoint = bus.GetSendEndpoint(_queuePortal).GetAwaiter().GetResult();
            }
            base.PreStart();
        }
        public static Props Prop()
        {
            return Props.Create(() => new ProcessorActor());
        }
        private async Task CreateAccount(CreateAccount command)
        {
            using (var context = Context.CreateScope())
            {
                var userManager = context.ServiceProvider.GetRequiredService<UserManager<HostelUser>>();
                var payload = command.Payload;
                var response = new Dictionary<string, string>();
                try
                {
                    var user = new HostelUser
                    {
                        UserName = payload["Email"],
                        Email = payload["Email"],
                        PhoneNumber = payload["Phone"],
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = false
                    };
                    var created = await userManager.CreateAsync(user, payload["Password"]);
                    if (created.Succeeded)
                    {
                        var userClaims = new List<Claim>();
                        var userRole = payload["Role"];
                        userClaims.Add(new Claim(JwtClaimTypes.Role, userRole));
                        userClaims.Add(new Claim("PreferredUserName", payload["Email"]));
                        var claim = await userManager.AddClaimsAsync(user, userClaims);
                        if (!claim.Succeeded)
                        {
                            await userManager.DeleteAsync(user);
                            response["Success"] = "false";
                            response["Errors"] = string.Join(", ", claim.Errors);
                            response["Message"] = "Your account created was aborted";
                        }
                        else
                        {
                            response["Success"] = "true";
                            response["Errors"] = string.Join(", ", created.Errors);
                            response["Message"] = "Your account created successfully!!";
                        }
                    }
                    else
                    {
                        response["Success"] = "false";
                        response["Errors"] = string.Join(", ", created.Errors);
                        response["Message"] = "Your account was not created";
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.ToString());
                    response["Success"] = "false";
                    response["Errors"] = e.Message;
                    response["Message"] = e.Message;
                    Console.ResetColor();
                }
                var @event = new MassTransitEvent("AccountCreated", command.Commander, command.CommandId, response);
                await SendToQueue(@event);
                await Self.GracefulStop(TimeSpan.FromSeconds(10));
            }
            
        }
        private async Task AddHostelClaim(AddHostelClaim command)
        {
            using (var context = Context.CreateScope())
            {
                var userManager = context.ServiceProvider.GetRequiredService<UserManager<HostelUser>>();
                var payload = command.Payload;
                var response = new Dictionary<string, string>();
                try
                {
                    var user = await userManager.FindByEmailAsync(payload["Email"]);
                    if (user != null)
                    {
                        var userClaims = new List<Claim>();
                        var userHostel = payload["Hostel"];
                        userClaims.Add(new Claim("Hostel", userHostel));
                        var claim = await userManager.AddClaimsAsync(user, userClaims);
                        if (!claim.Succeeded)
                        {
                            response["Created"] = "false";
                            response["Errors"] = string.Join(", ", claim.Errors);
                            response["Message"] = "Your account created was aborted";
                        }
                    }
                    else
                    {
                        response["Created"] = "false";
                        response["Errors"] = $"No user with '{payload["Email"]}' was found!";
                        response["Message"] = $"No user with '{payload["Email"]}' was found!";
                    }
                }
                catch (Exception e)
                {
                    response["Success"] = "false";
                    response["Errors"] = e.Message;
                    response["Message"] = e.Message;
                }
                var @event = new MassTransitEvent("AddedHostelClaim", command.Commander, command.CommandId, response);
                await SendToPortalQueue(@event);
                await Self.GracefulStop(TimeSpan.FromSeconds(10));
            }                
        }
        private async Task SendToQueue(IMassTransitEvent evnt)
        {
            if (_homeSendEndPoint != null)
            {
                await _homeSendEndPoint.Send(evnt);
            }
            else
            {
                using (var context = Context.CreateScope())
                {
                    var bus = context.ServiceProvider.GetService<IBusControl>();
                    _homeSendEndPoint = await bus.GetSendEndpoint(_queue);
                    await _homeSendEndPoint.Send(evnt);
                }
            }
            
        }
        private async Task SendToPortalQueue(IMassTransitEvent evnt)
        {
            if (_portalSendEndPoint != null)
            {
                await _portalSendEndPoint.Send(evnt);
            }
            else
            {
                using (var context = Context.CreateScope())
                {
                    var bus = context.ServiceProvider.GetService<IBusControl>();
                    _portalSendEndPoint = await bus.GetSendEndpoint(_queuePortal);
                    await _portalSendEndPoint.Send(evnt);
                }
            }

        }
    }
}
