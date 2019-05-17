using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SignalR.Host
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
            .UseStartup<Startup>()
            .Build();
            host.Run();
        }

    }
}
