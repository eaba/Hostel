using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace IdentityServer.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = null;
            var host = WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                //var configuration = config.Build();
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                configuration = config.Build();
            })
            //We need to do this since we are not using localhost but configuring the host file on windows
            //To easily edit host file I used HostsMan => https://github.com/portapps/hostsman-portable
            .UseHttpSys(options =>
            {
                options.Authentication.Schemes = Microsoft.AspNetCore.Server.HttpSys.AuthenticationSchemes.None;
                options.Authentication.AllowAnonymous = true;
                options.UrlPrefixes.Add("http://login.hostel.com");
                options.UrlPrefixes.Add("https://login.hostel.com");
            })
            //.UseIISIntegration()
            .UseUrls("https://login.hostel.com:443", "http://login.hostel.com:80")
            .UseStartup<Startup>()
            .Build();
            //SeedData.EnsureSeedData(host.Services, configuration);
            host.Run();
        }
    }
}
