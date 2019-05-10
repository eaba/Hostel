using Hostel.Command.Create;
using Hostel.Event.Created;
using Hostel.Repository;
using Hostel.State.Floor.Units;
using Shared;
using Shared.Repository;

namespace Hostel.Entity.Handler.Floor.Units
{
    public class BathRoomHandler : ICommandHandler<BathRoomState>
    {
        public HandlerResult Handle(BathRoomState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch (command)
            {
                case CreateBathRoom bathroom:
                    {
                        if (repository.CreateBathRoom(bathroom.BathRoom))
                        {
                            return new HandlerResult(new CreatedBathRoom(bathroom.BathRoom));
                        }
                        return new HandlerResult($"BathRoom {bathroom.BathRoom.Tag} could not be created at this time!", "", "");
                    }
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
