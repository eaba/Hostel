using Akka.Actor;
using Hostel.State.Floor.Units;
using Shared;
using Shared.Actors;

namespace Hostel.Entity.Floor.Units
{
    public class ToiletActor : HostelActor<ToiletState>
    {
        private string _connectionString;
        public ToiletActor(ICommandHandler<ToiletState> handler, ToiletState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
        {
            _connectionString = connectionString;
        }
        protected override void PreStart()
        {

            base.PreStart();
        }
        public static Props Prop(ICommandHandler<ToiletState> handler, ToiletState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new ToiletActor(handler, defaultState, persistenceId, connectionString));
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
