using Hostel.Command;
using Hostel.Event;
using Hostel.Model;
using Hostel.Repository;
using Hostel.State;
using Shared;
using Shared.Repository;

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
                        var floor = createFloor.Floor;
                        if (repository.CreateFloor(floor))
                        {
                            return new HandlerResult(new CreatedFloor(floor));
                        }
                        return new HandlerResult($"Floor {floor.Tag} could not be created at this time!", createFloor.Commander, createFloor.CommandId);
                    }
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
