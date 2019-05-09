using Akka.Actor;
using Hostel.Command;
using Hostel.Entity.Floor;
using Hostel.Entity.Handler;
using Hostel.Event;
using Hostel.State;
using Hostel.State.Floor;
using Shared;
using Shared.Actors;

namespace Hostel.Entity
{
    public class HostelManagerActor:HostelActor<HostelManagerState>
    {
        private string _connectionString;
        public HostelManagerActor(ICommandHandler<HostelManagerState> handler, HostelManagerState defaultState, string persistenceId, string connectionstring)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionstring))
        {
            _connectionString = connectionstring;  
        }
        protected override void OnRecoverComplete()
        {
            if (State.Constructed)
            {
                var hostel = State.ConstructionRecord;
                Self.Tell(new ConstructHostel(hostel));
            }
            base.OnRecoverComplete();
        }
        protected override void OnPersist(IEvent persistedEvent)
        {
            switch(persistedEvent)
            {
                case ConstructedHostel hostel:
                    {
                        var construct = hostel.Construction;
                        foreach (var floor in construct.Floors)
                        {
                            floor.HostelId = hostel.Construction.Detail.HostelId;
                            var createFloor = new CreateFloor(floor);
                            Self.Tell(createFloor);
                        }
                        var septicSpec = construct.SepticTank;
                        septicSpec.HostelId = hostel.Construction.Detail.HostelId;
                        var septic = new CreateSepticTank(septicSpec);
                        var waterSpec = construct.Reservoir;
                        waterSpec.HostelId = hostel.Construction.Detail.HostelId; ;
                        var water = new CreateWaterReservoir(waterSpec);
                        Self.Tell(septic);
                        Self.Tell(water);
                    }
                    break;
                case CreatedFloor createdFloor:
                    {
                        var floor = createdFloor.Floor;
                        floor.HostelId = State.ConstructionRecord.Detail.HostelId;
                        Context.ActorOf(FloorActor.Prop(new FloorHandler(), floor, FloorState.Empty, floor.Tag, _connectionString), floor.Tag);
                    }
                    break;
                case CreatedSepticTank createdSepticTank:
                    {
                        var septic = createdSepticTank.SepticTankSpec;
                        septic.HostelId = State.ConstructionRecord.Detail.HostelId;
                        var septicState = new SepticTankState(septic.SepticTankId, septic.Height, septic.AlertHeight, septic.Sensors);
                        var septicActor = Context.ActorOf(SepticTankActor.Prop(new SepticTankHandler(), septicState, septic.Tag, _connectionString), septic.Tag);
                        septicActor.Tell(new InstallSensor());
                    }
                    break;
                case CreatedWaterReservoir createdWater:
                    {
                        var water = createdWater.ReservoirSpec;
                        water.HostelId = State.ConstructionRecord.Detail.HostelId;
                        Context.ActorOf(WaterReservoirActor.Prop(new WaterReservoirHandler(), water.Sensors, WaterReservoirState.Empty, water.Tag, _connectionString), water.Tag);
                    }
                    break;
            }
            base.OnPersist(persistedEvent);
        }
        public static Props Prop(ICommandHandler<HostelManagerState> handler, HostelManagerState defaultState, string persistenceId, string connectionstring)
        {
            return Props.Create(() => new HostelManagerActor(handler, defaultState, persistenceId, connectionstring));
        }
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(maxNrOfRetries: 100, withinTimeMilliseconds: 1000, loggingEnabled: true,
                decider: Decider.From(x =>
                {
                    return Directive.Restart;
                }));
        }
        private void PrepareCommand(IMassTransitCommand command)
        {
            switch(command.Command.ToLower())
            {
               
            }
        }
    }
}
