using Hostel.Command;
using Hostel.Event;
using Hostel.Repository;
using Hostel.State;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler
{
    public class HostelManagerHandler : ICommandHandler<HostelManagerState>
    {
        public HandlerResult Handle(HostelManagerState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch (command)
            {
                case CreateFloor createFloor:
                    {
                        var floor = createFloor;
                        if (repository.CreateFloor(floor, out var id))
                        {
                            return new HandlerResult(new CreatedFloor(new Model.Floor(Guid.Parse(id), floor.Floor.Tag), floor.Commander, floor.CommandId));
                        }
                        return new HandlerResult($"Floor {floor.Floor.Tag} could not be created at this time!", floor.Commander, floor.CommandId);
                    }
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
