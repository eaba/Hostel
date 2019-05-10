using Akka.Actor;
using Hostel.Command;
using Hostel.Command.Create;
using Hostel.Command.Internal;
using Hostel.Entity.Floor.Units;
using Hostel.Entity.Handler;
using Hostel.Event.Created;
using Hostel.State.Floor;
using Hostel.State.Floor.Units;
using Shared;
using Shared.Actors;

namespace Hostel.Entity.Floor
{
    public class FloorActor : HostelActor<FloorState>
    {
        private string _connectionString;
        public FloorActor(ICommandHandler<FloorState> handler, FloorState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
        {
            _connectionString = connectionString;
            Command<LayoutFloor>(_=> {
                CreateChildren(State);
            });
        }
        protected override void PreStart()
        {
            //go through spec and generate commands here to self
            base.PreStart();
        }
        protected override void OnRecoverComplete()
        {
            base.OnRecoverComplete();
        }
        protected override void OnSnapshotOffer(FloorState state)
        {
            CreateChildren(state);
            base.OnSnapshotOffer(state);
        }
        protected override void OnPersist(IEvent persistedEvent)
        {            
            switch (persistedEvent)
            {
                case CreatedKitchen createdKitchen:
                    {
                        var kitchen = createdKitchen.Kitchen;
                        if(Context.Child(kitchen.Tag).IsNobody())
                        {
                            var km = Context.ActorOf(KitchenActor.Prop(new KitchenHandler(), new KitchenState(kitchen.Tag, State.FloorSpec.Kitchen.Sensors), kitchen.Tag, _connectionString), kitchen.Tag);
                            km.Tell(new InstallSensor());
                        }
                    }
                    break;
            }
            base.OnPersist(persistedEvent);
        }
        public static Props Prop(ICommandHandler<FloorState> handler, FloorState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new FloorActor(handler, defaultState, persistenceId, connectionString));
        }
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(maxNrOfRetries: 100, withinTimeMilliseconds: 1000, loggingEnabled: true,
                decider: Decider.From(x =>
                {
                    return Directive.Restart;
                }));
        }
        private void CreateChildren(FloorState state)
        {
            var kitchenSpec = State.FloorSpec.Kitchen;
            var createKitchen = new CreateKitchen(kitchenSpec);
            Self.Tell(createKitchen);

            var managerTag = $"{State.FloorSpec.Tag}-BathRoom-Manager";
            if(Context.Child(managerTag).IsNobody())
            {
                var bm = Context.ActorOf(BathRoomManagerActor.Prop(new BathRoomManagerHandler(), new BathRoomManagerState(State.FloorSpec.BathRooms, managerTag), managerTag, _connectionString), managerTag);
                bm.Tell(new LayoutBathRoom());
            }
            var roomManagerTag = $"{State.FloorSpec.Tag}-Room-Manager";
            if (Context.Child(roomManagerTag).IsNobody())
            {
                var rm = Context.ActorOf(RoomManagerActor.Prop(new RoomManagerHandler(), new RoomManagerState(State.FloorSpec.Rooms, roomManagerTag), roomManagerTag, _connectionString), roomManagerTag);
                rm.Tell(new LayoutRoom());
            }
            var toiletManagerTag = $"{State.FloorSpec.Tag}-Toilet-Manager";
            if (Context.Child(toiletManagerTag).IsNobody())
            {
                var tm = Context.ActorOf(ToiletManagerActor.Prop(new ToiletManagerHandler(), new ToiletManagerState(State.FloorSpec.Toilets, toiletManagerTag), toiletManagerTag, _connectionString), toiletManagerTag);
                tm.Tell(new LayoutToilet());
            }
            
        }
    }
}
