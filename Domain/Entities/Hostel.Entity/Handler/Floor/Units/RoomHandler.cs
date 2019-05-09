using Hostel.State.Floor.Units;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler
{
    public class RoomHandler : ICommandHandler<RoomState>
    {
        public HandlerResult Handle(RoomState state, ICommand command, IRepository<IDbProperties> repository)
        {
            throw new NotImplementedException();
        }
    }
}
