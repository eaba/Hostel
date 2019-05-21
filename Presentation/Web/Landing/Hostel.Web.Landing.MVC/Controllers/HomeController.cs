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
using System.Net.Http;

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
