using Akka.Actor;
using Hostel.State;
using Shared;
using Shared.Actors;

namespace Hostel.Entity
{
    public class SepticTankActor: HostelActor<SepticTankState>
    {
        public SepticTankActor(ICommandHandler<SepticTankState> handler, SepticTankState defaultState, string persistenceId, string connectstring)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectstring))
        {
            
        }
        public static Props Prop(ICommandHandler<SepticTankState> handler, SepticTankState defaultState, string persistenceId, string connectstring)
        {
            return Props.Create(() => new SepticTankActor(handler, defaultState, persistenceId, connectstring));
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
