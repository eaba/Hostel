using Hostel.Command;
using Hostel.Event;
using Hostel.Repository;
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
                case CreateFloor createFloor:
                    {
                        var floor = createFloor.Floor;
                        if (!state.Alive)
                        {                            
                            if (repository.CreateFloor(floor))
                            {
                                return new HandlerResult(new CreatedFloor(floor));
                            }
                            return new HandlerResult($"Floor {floor.Tag} could not be created at this time!", createFloor.Commander, createFloor.CommandId);
                        }
                        return new HandlerResult($"Floor {floor.Tag} alread exist. Did the government demonish it?", createFloor.Commander, createFloor.CommandId);
                    }
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
