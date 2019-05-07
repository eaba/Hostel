using Akka.Actor;
using Hostel.State;
using Shared;
using Shared.Actors;
using Shared.Repository;

namespace Hostel.Entity
{
    public class SepticTankActor: HostelActor<SepticTankState>
    {
        public SepticTankActor(ICommandHandler<SepticTankState> handler, SepticTankState defaultState, string persistenceId, IRepository<IDbProperties> repository)
            : base(handler, defaultState, persistenceId, repository)
        {
            
        }
        public static Props Prop(ICommandHandler<SepticTankState> handler, SepticTankState defaultState, string persistenceId, IRepository<IDbProperties> repository)
        {
            return Props.Create(() => new SepticTankActor(handler, defaultState, persistenceId, repository));
        }
    }
}
