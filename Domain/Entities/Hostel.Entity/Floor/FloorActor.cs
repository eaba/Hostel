using Akka.Actor;
using Hostel.Command;
using Hostel.Command.Internal;
using Hostel.Entity.Floor.Units;
using Hostel.Entity.Handler;
using Hostel.Entity.Handler.Floor;
using Hostel.State.Floor;
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
        protected override void OnPersist(IEvent persistedEvent)
        {            
            switch (persistedEvent)
            {
                
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
            var managerTag = $"{State.FloorSpec.Tag}-BathRoom-Manager";
            if(Context.Child(managerTag).IsNobody())
            {
                var bm = Context.ActorOf(BathRoomManagerActor.Prop(new BathRoomManagerHandler(), new KitchenManagerState(State.FloorSpec.Kitchen.Sensors, managerTag), managerTag, _connectionString), managerTag);
                bm.Tell(new LayoutBathRoom());
            }
            var toiletManagerTag = $"{State.FloorSpec.Tag}-Toilet-Manager";
            if (Context.Child(toiletManagerTag).IsNobody())
            {
                var tm = Context.ActorOf(ToiletManagerActor.Prop(new ToiletManagerHandler(), new ToiletManagerState(State.FloorSpec.Toilets, toiletManagerTag), toiletManagerTag, _connectionString), toiletManagerTag);
                tm.Tell(new LayoutToilet());
            }
            var kitchenTag = State.FloorSpec.Kitchen.Tag;
            if (Context.Child(kitchenTag).IsNobody())
            {
                var km = Context.ActorOf(KitchenActor.Prop(new KitchenManagerHandler(), new KitchenManagerState(State.FloorSpec.Kitchen.Sensors, kitchenTag), kitchenTag, _connectionString), kitchenTag);
                km.Tell(new InstallSensor());
            }
        }
    }
}
