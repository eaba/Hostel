using Akka.Actor;
using Hostel.Command;
using Hostel.Command.StateChange.Floor;
using Hostel.Entity.Floor;
using Hostel.Entity.Handler;
using Hostel.Event;
using Hostel.State;
using Hostel.State.Floor;
using Shared;
using Shared.Actors;
using System;
using System.Linq;

namespace Hostel.Entity
{
    public class HostelManagerActor:HostelActor<HostelManagerState>
    {
        private string _connectionString;
        public HostelManagerActor(ICommandHandler<HostelManagerState> handler, HostelManagerState defaultState, string persistenceId, string connectionstring)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionstring))
        {
            _connectionString = connectionstring;            
            Command<StoreFloorStateChange>(state => 
            {
                var floorStates = State.FloorStates.ToList();
                var floor = floorStates.FirstOrDefault(x => x.Tag == state.State.Tag);
                var index = floorStates.IndexOf(floor);
                floorStates.RemoveAt(index);
                floorStates.Add(state.State);
                State.FloorStates = floorStates;
                SaveSnapshot(State);
            });
        }
        protected override void OnRecoverComplete()
        {
            var floors = State.FloorStates.ToList();
            foreach(var floor in floors)
            {
                var tag = floor.Tag;
                var child = Context.Child(tag);
                if(child.IsNobody())
                {
                    //Context.ActorOf(FloorActor.Prop(new FloorHandler(), floor., FloorState.Empty, tag, _connectionString), tag);
                }
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
