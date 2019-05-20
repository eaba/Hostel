
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Hostel.Web.Landing.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                //var configuration = config.Build();
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            //We need to do this since we are not using localhost but configuring the host file on windows
            //To easily edit host file I used HostsMan => https://github.com/portapps/hostsman-portable
            .UseHttpSys(options =>
            {
                options.Authentication.Schemes = Microsoft.AspNetCore.Server.HttpSys.AuthenticationSchemes.None;
                options.Authentication.AllowAnonymous = true;
                options.UrlPrefixes.Add("http://hostel.com");
                options.UrlPrefixes.Add("https://hostel.com");
            })
            //.UseIISIntegration()
            .UseUrls("https://hostel.com:443", "http://hostel.com:80")
            .UseStartup<Startup>()
            .Build();
            host.Run();
        }
    }
}
