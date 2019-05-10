using Hostel.State.Floor.Units;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler.Floor.Units
{
    public class BathRoomHandler : ICommandHandler<BathRoomState>
    {
        public HandlerResult Handle(BathRoomState state, ICommand command, IRepository<IDbProperties> repository)
        {
            throw new NotImplementedException();
        }
    }
}
