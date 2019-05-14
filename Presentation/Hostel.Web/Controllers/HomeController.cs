using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hostel.Web.Controllers
{
    [Route("api/home")]
    public class HomeController : Controller
    {
        private readonly IBusControl _bus;
        private ISendEndpoint sendEndpoint;
        public HomeController(IBusControl bus)
        {
            _bus = bus;
        }
        [Route("person")]
        [HttpPost]
        public async Task<JsonResult> AccountApi([FromBody]Dictionary<string, string> m)
        {
            try
            {
                var requestid = m["Request"];
                var queue = "churchos_database_account";
                var message = new Dictionary<string, string>
                {
                    ["Action"] = action,
                    ["Username"] = username,
                    ["Client"] = client,
                    ["Request"] = requestid,
                };
                var messageData = JsonConvert.SerializeObject(m, Formatting.Indented);
                message["Data"] = messageData;
                await SendToQueue(queue, dto);
                var x = JsonConvert.SerializeObject(new Dictionary<string, object> { { "Request", requestid }, { "Success", 1 }, { "Message", "Your request has been sent...we are working on it!!" } });
                return Json(x);
            }
            catch (Exception e)
            {
                var d = new Dictionary<string, object> { { "Request", m["Request"] }, { "Success", 0 }, { "Message", "0000: " + e.Message } };
                return Json(JsonConvert.SerializeObject(d, Formatting.Indented));
            }
        }
    }
}
