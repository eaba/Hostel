using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hostel.Web.Landing.MVC.Models;

namespace Hostel.Web.Landing.MVC.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {        
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
