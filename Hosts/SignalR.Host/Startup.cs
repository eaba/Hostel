using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalR.Host.Consumers;
using SignalR.Host.Hubs;
using System;

namespace SignalR.Host
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR(options =>{ options.KeepAliveInterval = TimeSpan.FromSeconds(5); });
            services.AddMassTransit(x =>
            {
                x.AddConsumer<HomeConsumer>();
                x.AddConsumer<PortalConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://localhost:/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint(host, "hostel_home_queue", e =>
                    {
                        e.PrefetchCount = 500;
                        e.UseRetry(r => r.Immediate(5));
                        e.Consumer<HomeConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint(host, "hostel_portal_queue", e =>
                    {
                        e.PrefetchCount = 500;
                        e.UseRetry(r => r.Immediate(5));
                        e.Consumer<PortalConsumer>(provider);
                    });
                }));
            });
            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, ServiceManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            //app.UseAuthentication();
            app.UseSignalR(routes =>
            {
                routes.MapHub<HomeHub>("/home");
                routes.MapHub<PortalHub>("/portal");
            });
        }
    }
}
