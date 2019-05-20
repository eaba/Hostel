using Ignite.SharpNetSH;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SignalR.Host
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
            //I created SSL cert using xca => https://github.com/chris2511/xca
            NetSH.CMD.Http.Add.UrlAcl("https://events.hostel.com:443/", "user", false, false);
            NetSH.CMD.Http.Add.SSLCert(hostnamePort: "events.hostel.com:443", certHash: "62b399ff1cd0d6807c9dd70e832ca17ca009a918", appId: Guid.Parse("921328e8-5cdf-4b7f-a3cc-4e50147d1521"), certStoreName: "my");
            await _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                NetSH.CMD.Http.Delete.SSLCert(hostnamePort: "events.hostel.com:443");
                NetSH.CMD.Http.Delete.UrlAcl("https://events.hostel.com:443");
                _busControl.StopAsync(cancellationToken);
            }
            catch { }
            return Task.CompletedTask;
        }
    }
}
