using Hostel.State.Floor;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler
{
    public class FloorHandler : ICommandHandler<FloorState>
    {
        public HandlerResult Handle(FloorState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch(command)
            {
                
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
