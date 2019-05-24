using MassTransit;
using MassTransit.Command;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hostel.Web.Landing.MVC.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("api")]
    public class Api : ControllerBase
    {
        private readonly IBusControl _bus;
        private ConcurrentDictionary<string, ISendEndpoint> CachedEndpoints = new ConcurrentDictionary<string, ISendEndpoint>();
        public Api(IBusControl bus)
        {
            _bus = bus;
        }
        [Route("person")]
        [HttpPost]
        public async Task<string> CreatePerson([FromBody]Dictionary<string, string> payload)
        {
            string command = string.Empty;
            try
            {
                command = payload["Command"];
                var commandid = payload["CommandId"];
                var commander = payload["Commander"];
                var payld = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload["Payload"]);
                var create = new MassTransitCommand("CreatePerson", commander, commandid, payld);
                await SendToQueue("hostel_queue", create);
                return $"{command}: Your request has been sent...we are working on it!!";
            }
            catch (Exception e)
            {
                return $"{command} failed with message:'{e.Message}'";

            }
        }
        [Route("account")]
        [HttpPost]
        public async Task<string> CreateAccount([FromBody]Dictionary<string, string> payload)
        {
            string command = string.Empty;
            try
            {
                command = payload["Command"];
                var commandid = payload["CommandId"];
                var commander = payload["Commander"];
                var payld = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload["Payload"]);
                var create = new MassTransitCommand("CreateAccount", commander, commandid, payld);
                await SendToQueue("hostel_identity_queue", create);
                return $"{command}: Your request has been sent...we are working on it!!";
            }
            catch (Exception e)
            {
                return $"{command} failed with message:'{e.Message}'";
            }
        }
        private async Task SendToQueue(string queue, IMassTransitCommand command)
        {
            if (CachedEndpoints.TryGetValue(queue, out var endPoint))
            {
                await endPoint.Send(command);
            }
            else
            {
                var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/" + queue));
                await endpoint.Send(command);
                CachedEndpoints.TryAdd(queue, endpoint);
            }
        }
    }
}
