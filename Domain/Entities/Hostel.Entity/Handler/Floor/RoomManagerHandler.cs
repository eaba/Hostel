using Hostel.State.Floor;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler.Floor
{
    public class RoomManagerHandler : ICommandHandler<RoomManagerState>
    {
        public HandlerResult Handle(RoomManagerState state, ICommand command, IRepository<IDbProperties> repository)
        {
            throw new NotImplementedException();
        }
    }
}
