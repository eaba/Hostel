using Hostel.Command.Create;
using Hostel.Event.Created;
using Hostel.Repository;
using Hostel.State.Floor.Units;
using Shared;
using Shared.Repository;

namespace Hostel.Entity.Handler.Floor.Units
{
    public class RoomHandler : ICommandHandler<RoomState>
    {
        public HandlerResult Handle(RoomState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch (command)
            {
                
                default: return HandlerResult.NotHandled(command);
            }
        }
    }
}
