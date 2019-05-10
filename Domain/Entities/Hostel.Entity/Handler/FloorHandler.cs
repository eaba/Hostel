using Hostel.Command.Create;
using Hostel.Event.Created;
using Hostel.Repository;
using Hostel.State.Floor;
using Shared;
using Shared.Repository;

namespace Hostel.Entity.Handler
{
    public class FloorHandler : ICommandHandler<FloorState>
    {
        public HandlerResult Handle(FloorState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch(command)
            {
                case CreateKitchen createKitchen:
                    {
                        var kitchen = createKitchen.Kitchen;
                        if (repository.CreateKitchen(kitchen))
                        {
                            return new HandlerResult(new CreatedKitchen(kitchen));
                        }
                        return new HandlerResult($"Kitchen {kitchen.Tag} could not be created at this time!", createKitchen.Commander, createKitchen.CommandId);
                    }
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
