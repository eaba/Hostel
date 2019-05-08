using Hostel.State;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler
{
    public class WaterReservoirHandler : ICommandHandler<WaterReservoirState>
    {
        public HandlerResult Handle(WaterReservoirState state, ICommand command, IRepository<IDbProperties> repository)
        {
            throw new NotImplementedException();
        }
    }
}
