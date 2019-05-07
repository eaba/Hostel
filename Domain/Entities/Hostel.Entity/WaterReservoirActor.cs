using Akka.Actor;
using Hostel.State;
using Shared;
using Shared.Actors;
using Shared.Repository;

namespace Hostel.Entity
    public class WaterReservoirActor: HostelActor<SepticTankState>
    {
        public WaterReservoirActor(ICommandHandler<SepticTankState> handler, SepticTankState defaultState, string persistenceId, IRepository<IDbProperties> repository)
            : base(handler, defaultState, persistenceId, repository)
        {

        }
        public static Props Prop(ICommandHandler<SepticTankState> handler, SepticTankState defaultState, string persistenceId, IRepository<IDbProperties> repository)
        {
            return Props.Create(() => new WaterReservoirActor(handler, defaultState, persistenceId, repository));
        }
    }
}
