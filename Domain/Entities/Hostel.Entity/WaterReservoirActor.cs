using Akka.Actor;
using Hostel.State;
using Shared;
using Shared.Actors;

namespace Hostel.Entity
{ 
    public class WaterReservoirActor: HostelActor<WaterReservoirState>
    {
        public WaterReservoirActor(ICommandHandler<WaterReservoirState> handler, WaterReservoirState defaultState, string persistenceId, string connectionstring)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionstring))
        {

        }
        public static Props Prop(ICommandHandler<WaterReservoirState> handler, WaterReservoirState defaultState, string persistenceId, string connectionstring)
        {
            return Props.Create(() => new WaterReservoirActor(handler, defaultState, persistenceId, connectionstring));
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
