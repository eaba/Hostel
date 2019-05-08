using Akka.Actor;
using Hostel.Entity.Handler.Floor;
using Hostel.Event;
using Hostel.Model;
using Hostel.State.Floor;
using Shared;
using Shared.Actors;

namespace Hostel.Entity.Floor
{
    public class FloorActor : HostelActor<FloorState>
    {
        private string _connectionString;
        private FloorSpec _floorSpec;
        public FloorActor(ICommandHandler<FloorState> handler, FloorSpec spec, FloorState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
        {
            _floorSpec = spec;
            _connectionString = connectionString;
        }
        protected override void PreStart()
        {
            //go through spec and generate commands here to self
            base.PreStart();
        }
        protected override void OnRecoverComplete()
        {
            CreateChildren(State.FloorSpec);
            base.OnRecoverComplete();
        }
        protected override void OnPersist(IEvent persistedEvent)
        {            
            switch (persistedEvent)
            {
                
            }
            base.OnPersist(persistedEvent);
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
        private void CreateChildren(FloorSpec spec)
        {
            var bathId = $"{spec.Tag}-BathManager";
            var child = Context.Child(bathId);
            if (child.IsNobody())
            {
                Context.ActorOf(BathRoomManagerActor.Prop(new BathRoomManagerHandler(), spec.BathRooms, BathRoomManagerState.Empty, bathId, _connectionString), bathId);
            }
        }
    }
}
