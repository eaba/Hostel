using Akka.Actor;
using Hostel.Command;
using Hostel.Command.Create;
using Hostel.Command.Internal;
using Hostel.Entity.Floor.Units;
using Hostel.Entity.Handler.Floor.Units;
using Hostel.Event.Created;
using Hostel.State.Floor;
using Hostel.State.Floor.Units;
using Shared;
using Shared.Actors;

namespace Hostel.Entity.Floor
{
    public class BathRoomManagerActor : HostelActor<BathRoomManagerState>
    {
        private string _connectionString;
        public BathRoomManagerActor(ICommandHandler<BathRoomManagerState> handler, BathRoomManagerState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
        {
            _connectionString = connectionString;
            Command<LayoutBathRoom>(bath => {
                CreateBathRooms(State);
            });
        }
        protected override void OnSnapshotOffer(BathRoomManagerState state)
        {
            CreateBathRooms(state);
            base.OnSnapshotOffer(state);
        }
        protected override void PreStart()
        {
            base.PreStart();
        }
        protected override void OnPersist(IEvent persistedEvent)
        {
            switch(persistedEvent)
            {
                case CreatedBathRoom bathroom:
                    {
                        var bath = bathroom.BathRoom;
                        if(Context.Child(bath.Tag).IsNobody())
                        {
                            var bathActor = Context.ActorOf(BathRoomActor.Prop(new BathRoomHandler(), new BathRoomState(bath.BathRoomId, bath.Tag, bath.Sensors), bath.Tag, _connectionString), bath.Tag);
                            bathActor.Tell(new InstallSensor());
                        }
                    }
                    break;
            }
            base.OnPersist(persistedEvent);
        }
        public static Props Prop(ICommandHandler<BathRoomManagerState> handler, BathRoomManagerState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new BathRoomManagerActor(handler, defaultState, persistenceId, connectionString));
        }
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(maxNrOfRetries: 100, withinTimeMilliseconds: 1000, loggingEnabled: true,
                decider: Decider.From(x =>
                {
                    return Directive.Restart;
                }));
        }
        private void CreateBathRooms(BathRoomManagerState state)
        {
            var bathRooms = state.BathRooms;
            foreach (var bath in bathRooms)
            {
                var createBathRoom = new CreateBathRoom(bath);
                Self.Tell(createBathRoom);
            }
        }
    }
}
