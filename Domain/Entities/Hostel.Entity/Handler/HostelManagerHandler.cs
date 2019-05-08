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
                
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
