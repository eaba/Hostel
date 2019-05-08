using Hostel.State;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler
{
    public class SepticTankHandler : ICommandHandler<SepticTankState>
    {
        public HandlerResult Handle(SepticTankState state, ICommand command, IRepository<IDbProperties> repository)
        {
            throw new NotImplementedException();
        }
    }
}
