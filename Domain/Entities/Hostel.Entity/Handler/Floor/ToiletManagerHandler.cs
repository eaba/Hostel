using Hostel.Command.Create;
using Hostel.Event.Created;
using Hostel.Repository;
using Hostel.State.Floor;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler.Floor
{
    public class ToiletManagerHandler : ICommandHandler<ToiletManagerState>
    {
        public HandlerResult Handle(ToiletManagerState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch(command)
            {
                case CreateToilet toilet:
                    {
                        if (repository.CreateToilet(toilet.Toilet))
                        {
                            return new HandlerResult(new CreatedToilet(toilet.Toilet));
                        }
                        return new HandlerResult($"Toilet {toilet.Toilet.Tag} could not be created at this time!", "", "");
                    }
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
