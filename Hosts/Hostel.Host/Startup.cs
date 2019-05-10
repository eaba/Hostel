using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hostel.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHostedService, HostelService>();
        }
        public IConfiguration Configuration { get; }
        public void Configure(IApplicationBuilder app)
        {
            //app.UseMvc();
            //app.ApplicationServices.GetService<AccountHostedService>().StartAsync(CancellationToken.None); //start MassTransit and Akka.NET
        }
    }
}
