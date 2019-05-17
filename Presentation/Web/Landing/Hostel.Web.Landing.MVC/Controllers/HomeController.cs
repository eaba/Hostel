using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hostel.Web.Landing.MVC.Models;
using Newtonsoft.Json;
using Shared;
using MassTransit.Command;
using MassTransit;
using System.Collections.Concurrent;

namespace Hostel.Web.Landing.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusControl _bus;
        private ConcurrentDictionary<string, ISendEndpoint> CachedEndpoints = new ConcurrentDictionary<string, ISendEndpoint>();
        public HomeController(IBusControl bus)
        {
            _bus = bus;
        }
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [Route("person")]
        [HttpPost]
        public async Task<JsonResult> CreatePerson([FromBody]Dictionary<string, string> payload)
        {
            try
            {
                var command = payload["Command"];
                var commandid = payload["CommandId"];
                var commander = payload["Commander"];
                var payld = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload["Payload"]);
                var create = new CreatePerson(command, commander, commandid, payld);
                await SendToQueue("hostel_queue", create);
                var x = JsonConvert.SerializeObject(new Dictionary<string, object> { { "Request", commandid }, { "Success", 1 }, { "Message", "Your request has been sent...we are working on it!!" } });
                return Json(x);
            }
            catch (Exception e)
            {
                var d = new Dictionary<string, object> { { "Request", payload["CommandId"] }, { "Success", 0 }, { "Message", "0000: " + e.Message } };
                return Json(JsonConvert.SerializeObject(d, Formatting.Indented));
            }
        }
        [Route("account")]
        [HttpPost]
        public async Task<JsonResult> CreateAccount([FromBody]Dictionary<string, string> payload)
        {
            try
            {
                var command = payload["Command"]; 
                var commandid = payload["CommandId"];
                var commander = payload["Commander"];
                var payld = JsonConvert.DeserializeObject<Dictionary<string, string>>(payload["Payload"]);
                var create = new CreatePerson(command, commander, commandid, payld);
                await SendToQueue("hostel_identity_queue", create);
                var x = JsonConvert.SerializeObject(new Dictionary<string, object> { { "Request", commandid }, { "Success", 1 }, { "Message", "Your request has been sent...we are working on it!!" } });
                return Json(x);
            }
            catch (Exception e)
            {
                var d = new Dictionary<string, object> { { "Request", payload["CommandId"] }, { "Success", 0 }, { "Message", "0000: " + e.Message } };
                return Json(JsonConvert.SerializeObject(d, Formatting.Indented));
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
