using Hostel.Command;
using Hostel.Event;
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
                case ConstructHostel construct:
                    {
                        var hostel = construct.Construction;
                        if (!state.Constructed)
                        {
                            if (repository.ConstructHostel(hostel))
                            {
                                return new HandlerResult(new ConstructedHostel(hostel));
                            }
                            return new HandlerResult($"Hostel {hostel.Detail.Name} could not be constructed at this time!", string.Empty, string.Empty);
                        }
                        return new HandlerResult($"Hostel {hostel.Detail.Name} alread exist. Did the government demonish your hostel?", string.Empty, string.Empty);
                    }
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
