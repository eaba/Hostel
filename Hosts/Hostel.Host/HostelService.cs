using Akka.Actor;
using Akka.Configuration;
using Akka.Extension;
using Akka.MassTransit.Logger;
using Hostel.Command;
using Hostel.Entity;
using Hostel.Entity.Handler;
using Hostel.Host.Observers;
using Hostel.Model;
using Hostel.State;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static Hostel.Model.Construction;

namespace Hostel.Host
{
    public class HostelService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private ActorSystem _actorSystem;
        private IBusControl _busControl;
        public HostelService(IBusControl busControl, IActorRef provider, ActorSystem actorSystem)
        {
            _busControl = busControl;
            HostActorRef.ActorRef = provider;
            _actorSystem = actorSystem;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //_actorSystem = _serviceProvider.GetService.;
            var observer = new ReceiveObserver(_actorSystem);
            _busControl.ConnectReceiveObserver(observer);
            await _busControl.StartAsync(cancellationToken);
            HostActorRef.ActorRef.Tell(Construct());
            HostActorRef.ActorIsReady = true;
            HostActorRef.ProcessCached();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                _busControl.StopAsync(cancellationToken);
                _actorSystem.Stop(HostActorRef.ActorRef);
                _actorSystem?.Dispose();
                _actorSystem = null;
            }
            catch { }
            return Task.CompletedTask;
        }
        private ConstructHostel Construct()
        {
            var construction = new Construction(new HostelDetail("Baafog", "Onikolobo Abeokuta, Ogun, Nigeria"))
                .WithFloor("Ground-Floor","", 20, "", 2, "T", 2, "B", "K")
                .WithFloor("First-Floor", "1", 22, "1", 3, "T1", 2, "B", "K")
                .WithFloor("Second-Floor", "1", 23, "1", 3, "T1", 2, "B", "K")
                .WithSepticTank("Septic", 10, 7)
                .WithReservoir("Reservoir", 20, 15);
            return new ConstructHostel(construction);
        }
    }
}
