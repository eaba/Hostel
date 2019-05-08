using Akka.Actor;
using Hostel.Entity.Handler.Floor;
using Hostel.Model;
using Hostel.State.Floor;
using Shared;
using Shared.Actors;

namespace Hostel.Entity.Floor
{
    public class FloorActor : HostelActor<FloorState>
    {
        private FloorSpec _floorSpec;
        public FloorActor(ICommandHandler<FloorState> handler, FloorSpec spec, FloorState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
        {
            _floorSpec = spec;
        }
        protected override void PreStart()
        {
            var BathId = $"{_floorSpec.Tag}-BathManager";
            var child = Context.Child(BathId);
            if (child.IsNobody())
            {
                Context.ActorOf(BathRoomManagerActor.Prop(new BathRoomManagerHandler(), _floorSpec.BathRooms, FloorState.Empty, tag, _connectionString), tag);
            }
            var bathrooms = _floorSpec.BathRooms;
            foreach(var bath in bathrooms)
            {
                
            var toilets = _floorSpec.Toilets;
            var rooms = _floorSpec.Rooms;
            var kichen = _floorSpec.Kitchen;
            base.PreStart();
        }
        public static Props Prop(ICommandHandler<FloorState> handler, FloorSpec spec, FloorState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new FloorActor(handler, spec, defaultState, persistenceId, connectionString));
        }
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(maxNrOfRetries: 100, withinTimeMilliseconds: 1000, loggingEnabled: true,
                decider: Decider.From(x =>
                {
                    return Directive.Restart;
                }));
        }
    }
}
