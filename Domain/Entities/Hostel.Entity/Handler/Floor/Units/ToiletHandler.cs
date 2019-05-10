using Hostel.State.Floor.Units;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler.Floor.Units
{
    public class ToiletHandler : ICommandHandler<ToiletState>
    {
        public HandlerResult Handle(ToiletState state, ICommand command, IRepository<IDbProperties> repository)
        {
            throw new NotImplementedException();
        }
    }
}
