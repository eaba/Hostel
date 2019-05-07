using Akka.Actor;
using Hostel.Command;
using Hostel.Command.StateChange.Floor;
using Hostel.Entity.Floor;
using Hostel.Entity.Handler;
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
        public HostelManagerActor(ICommandHandler<HostelManagerState> handler, HostelManagerState defaultState, string persistenceId, string connectionstring)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionstring))
        {
            Command<CreateFloor>(floor => 
            {
                var tag = floor.Floor.Tag;
                if(Context.Child(tag).IsNobody())
                {
                    Context.ActorOf(FloorActor.Prop(new FloorHandler(), FloorState.Empty, tag, Repo), tag);
                }
            });
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
                    Context.ActorOf(FloorActor.Prop(new FloorHandler(), FloorState.Empty, tag, Repo), tag);
                }
            }
            base.OnRecoverComplete();
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
                case "createfloor":
                    {
                        var floor = new Model.Floor(Guid.NewGuid(), $"Floor_{command.Payload["Tag"]}");
                        var createFloor = new CreateFloor(floor, command.Commander, command.CommandId);
                        Self.Tell(createFloor, Self);
                    }
                    break;
            }
        }
    }
}
