using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hostel.Web.Landing.MVC
{
    public class ServiceManager : IHostedService
    {
        private IBusControl _busControl;
        public ServiceManager(IBusControl busControl)
        {
            _busControl = busControl;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //netsh http add urlacl url=http://hostel.com:80/ user=Everyone
            //netsh http add urlacl url=https://hostel.com:443/ user=Everyone
            //netsh http add sslcert hostnameport=hostel.com:443 certhash=fe529d4025da295adc3add97c780454a10e9c41a appid={64813f3e-afe6-4426-ba50-1b56091064c6} certstorename=MY
            await _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                _busControl.StopAsync(cancellationToken);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return Task.CompletedTask;
        }
    }
}
