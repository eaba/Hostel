using MassTransit;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer.Host.Services
{ 
    public class IdentityService : IHostedService
    {
        private readonly IBusControl _busControl;

        public IdentityService(IBusControl busControl)
        {
            _busControl = busControl;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //netsh http add urlacl url=http://login.hostel.com:80/ user=Everyone
            //netsh http add urlacl url=https://login.hostel.com:443/ user=Everyone
            //netsh http add sslcert hostnameport=login.hostel.com:443 certhash=6a75ad35c06acbffaaad23e086d117f5cde16045 appid={64813f3e-afe6-4426-ba50-1b56091064c6} certstorename=MY
            return _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }
    }
}
