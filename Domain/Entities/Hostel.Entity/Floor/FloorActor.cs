using Akka.Actor;
using Hostel.State.Floor;
using Shared;
using Shared.Actors;
using Shared.Repository;

namespace Hostel.Entity.Floor
{
    public class FloorActor : HostelActor<FloorState>
    {
        public FloorActor(ICommandHandler<FloorState> handler, FloorState defaultState, string persistenceId, IRepository<IDbProperties> repository)
            : base(handler, defaultState, persistenceId, repository)
        {
            
        }
        public static Props Prop(ICommandHandler<FloorState> handler, FloorState defaultState, string persistenceId, IRepository<IDbProperties> repository)
        {
            return Props.Create(() => new FloorActor(handler, defaultState, persistenceId, repository));
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
