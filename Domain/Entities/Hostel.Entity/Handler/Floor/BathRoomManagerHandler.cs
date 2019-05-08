using Hostel.State.Floor;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler.Floor
{
    public class BathRoomManagerHandler : ICommandHandler<BathRoomManagerState>
    {
        public HandlerResult Handle(BathRoomManagerState state, ICommand command, IRepository<IDbProperties> repository)
        {
            throw new NotImplementedException();
        }
    }
}
