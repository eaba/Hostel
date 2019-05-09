using Akka.Actor;
using Hostel.Command.Internal;
using Hostel.State.Floor;
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
            foreach (var baths in bathRooms)
            {
                //send createbathroom command to self
            }
        }
    }
}
