using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace Hostel.Web.Portal.MVC.Controllers
{
    [Authorize("tenant")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("api")]
    [ValidateAntiForgeryToken]
    public class ApiTenant : ControllerBase
    {
        private readonly IBusControl _bus;
        private ConcurrentDictionary<string, ISendEndpoint> CachedEndpoints = new ConcurrentDictionary<string, ISendEndpoint>();
    }
}
