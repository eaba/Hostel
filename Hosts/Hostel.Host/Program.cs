using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Hostel.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                var configuration = config.Build();
            })
            .UseStartup<Startup>()
            .Build();
            host.Run();
        }
    }
}
