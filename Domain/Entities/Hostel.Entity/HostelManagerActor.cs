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
                            var createFloor = new CreateFloor(floor);
                            Self.Tell(createFloor);
                        }
                        var tank = construct.SepticTank;
                        var reservoir = construct.Reservoir;
                        var septic = new CreateSepticTank(tank.Tag, tank.Height, tank.Sensors);
                        var water = new CreateWaterReservoir(reservoir.Tag, reservoir.Height, reservoir.Sensors);
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
