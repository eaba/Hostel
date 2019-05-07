using Akka.Actor;
using Akka.Configuration;
using Akka.MassTransit.Logger;
using Hostel.Command;
using Hostel.Entity;
using Hostel.Entity.Handler;
using Hostel.Host.Observers;
using Hostel.Model;
using Hostel.State;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Hostel.Host
{
    public class HostelService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private ActorSystem _actorSystem;
        public HostelService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var hoconFile = File.ReadAllText("host.hocon");
            var hoconConfig = ConfigurationFactory.ParseString(hoconFile);
            _actorSystem = ActorSystem.Create("HostelActorSystem", hoconConfig);
            BusCreator.BusControl = BusCreator.CreateBus(_configuration, _serviceProvider);
            var observer = new ReceiveObserver(_actorSystem);
            BusCreator.BusControl.ConnectReceiveObserver(observer);
            await BusCreator.BusControl.StartAsync(cancellationToken);
            AkkaService.Bus = BusCreator.BusControl;
            HostActorRef.ActorRef = _actorSystem.ActorOf(HostelManagerActor.Prop(new HostelManagerHandler(), HostelManagerState.Empty, "HostelManager", _configuration.GetConnectionString("Database")), "HostelManager");
            HostActorRef.ActorRef.Tell(Construct());
            HostActorRef.ActorIsReady = true;
            HostActorRef.ProcessCached();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                BusCreator.BusControl.StopAsync(cancellationToken);
                _actorSystem.Stop(HostActorRef.ActorRef);
                _actorSystem?.Dispose();
                _actorSystem = null;
            }
            catch { }
            return Task.CompletedTask;
        }
        private ConstructHostel Construct()
        {
            var construction = new Construction()
                .WithFloor("Ground-Floor","", 20, "", 2, "T", 2, "B", "K")
                .WithFloor("First-Floor", "1", 22, "1", 3, "T1", 2, "B", "K")
                .WithFloor("Second-Floor", "1", 23, "1", 3, "T1", 2, "B", "K")
                .WithSepticTank("Septic", 100)
                .WithReservoir("Reservoir", 500);
            return new ConstructHostel(construction);
        }
    }
}
