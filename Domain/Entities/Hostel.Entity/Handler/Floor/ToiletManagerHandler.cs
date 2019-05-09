using Hostel.State.Floor;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler
{
    public class ToiletManagerHandler : ICommandHandler<ToiletManagerState>
    {
        public HandlerResult Handle(ToiletManagerState state, ICommand command, IRepository<IDbProperties> repository)
        {
            throw new NotImplementedException();
        }
    }
}
