using Hostel.State.Floor.Units;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler
{
    public class KitchenHandler : ICommandHandler<KitchenState>
    {
        public HandlerResult Handle(KitchenState state, ICommand command, IRepository<IDbProperties> repository)
        {
            throw new NotImplementedException();
        }
    }
}
