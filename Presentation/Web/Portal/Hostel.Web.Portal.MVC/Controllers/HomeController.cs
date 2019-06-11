using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hostel.Web.Portal.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Hostel.Web.Portal.MVC.Controllers
{
    [Authorize]
    [Route("/")]
    [ValidateAntiForgeryToken]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("Authobject")]
        public async Task<string> AuthObject()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var role = User.Claims.FirstOrDefault(x => x.Type.ToLower() == "role").Value;
            var authObject = new Dictionary<string, string>
            {
                {"Token", accessToken},
                {"Role", role}
            };
            return JsonConvert.SerializeObject(authObject, Formatting.Indented);
        }
        [Route("Logout")]
        public async Task<ActionResult> Logout()
        {
            var user = User as ClaimsPrincipal;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            return new SignOutResult(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "https://login.churchos.io" });
        }
    }
}
