using Akka.Actor;
using Shared;
using Shared.Actors;

namespace IdentityServer.Host
{
    public class IdentityActor:HostelActor<IdentityState>
    {
        private string _connectionString;
        public IdentityActor(ICommandHandler<IdentityState> handler, IdentityState defaultState, string persistenceId, string connectionstring)
            : base(handler, defaultState, persistenceId, connectionstring)
        {
            _connectionString = connectionstring;
        }
        public static Props Prop(ICommandHandler<IdentityState> handler, IdentityState defaultState, string persistenceId, string connectstring)
        {
            return Props.Create(() => new IdentityActor(handler, defaultState, persistenceId, connectstring));
        }
        protected override void OnPersist(IEvent persistedEvent)
        {            
            base.OnPersist(persistedEvent);
        }
        protected override void OnSnapshotOffer(IdentityState state)
        {            
            base.OnSnapshotOffer(state);
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
