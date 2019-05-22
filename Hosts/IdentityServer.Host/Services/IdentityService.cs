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
            return _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }
    }
}
