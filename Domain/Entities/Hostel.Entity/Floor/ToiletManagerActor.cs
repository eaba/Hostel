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
    public class ToiletManagerActor : HostelActor<ToiletManagerState>
    {
        private string _connectionString;
        public ToiletManagerActor(ICommandHandler<ToiletManagerState> handler, ToiletManagerState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, connectionString)
        {
            _connectionString = connectionString;
            Command<LayoutToilet>(toilet => {
                CreateToilets(State);
            });
        }
        protected override void PreStart()
        {            
            base.PreStart();
        }
        protected override void OnPersist(IEvent persistedEvent)
        {
            switch (persistedEvent)
            {
                case CreatedToilet toilet:
                    {
                        var tolet = toilet.Toilet;
                        if (Context.Child(tolet.Tag).IsNobody())
                        {
                            var toiletActor = Context.ActorOf(ToiletActor.Prop(new ToiletHandler(), new ToiletState(tolet.ToiletId, tolet.Tag, tolet.Sensors), tolet.Tag, _connectionString), tolet.Tag);
                            toiletActor.Tell(new InstallSensor());
                        }
                    }
                    break;
            }
            base.OnPersist(persistedEvent);
        }
        protected override void OnSnapshotOffer(ToiletManagerState state)
        {
            CreateToilets(state);
            base.OnSnapshotOffer(state);
        }
        public static Props Prop(ICommandHandler<ToiletManagerState> handler, ToiletManagerState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new ToiletManagerActor(handler, defaultState, persistenceId, connectionString));
        }
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(maxNrOfRetries: 100, withinTimeMilliseconds: 1000, loggingEnabled: true,
                decider: Decider.From(x =>
                {
                    return Directive.Restart;
                }));
        }
        private void CreateToilets(ToiletManagerState state)
        {
            var toilets = state.Toilets;
            foreach (var toilet in toilets)
            {
                toilet.FloorId = state.FloorId;
                var createToilet = new CreateToilet(toilet);
                Self.Tell(createToilet);
            }
        }
    }
}
