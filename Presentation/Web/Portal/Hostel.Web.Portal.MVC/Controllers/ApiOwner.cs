using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Hostel.Web.Portal.MVC.Controllers
{
    [Authorize]
    [EnableCors("CorsPolicy")]
    [ApiController]
    [Route("api")]
    public class ApiOwner : ControllerBase
    {
    }
}
