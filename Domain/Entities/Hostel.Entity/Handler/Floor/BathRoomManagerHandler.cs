using Hostel.Command.Create;
using Hostel.Event.Created;
using Hostel.Repository;
using Hostel.Repository.Write;
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
            switch(command)
            {
                case CreateBathRoom bathroom:
                    {
                        if (repository.CreateBathRoom(bathroom.BathRoom))
                        {
                            return new HandlerResult(new CreatedBathRoom(bathroom.BathRoom));
                        }
                        return new HandlerResult($"BathRoom {bathroom.BathRoom.Tag} could not be created at this time!");
                    }
                default: return HandlerResult.NotHandled(command);
            }
        }
    }
}