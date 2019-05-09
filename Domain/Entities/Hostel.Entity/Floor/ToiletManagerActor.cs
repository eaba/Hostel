using Akka.Actor;
using Hostel.Command.Internal;
using Hostel.State.Floor;
using Shared;
using Shared.Actors;

namespace Hostel.Entity.Floor
{
    public class ToiletManagerActor : HostelActor<ToiletManagerState>
    {
        private string _connectionString;
        public ToiletManagerActor(ICommandHandler<ToiletManagerState> handler, ToiletManagerState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
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
            foreach (var tlt in toilets)
            {
                //send createtoilet command to self
            }
        }
    }
}
