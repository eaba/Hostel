using Hostel.Command.Create;
using Hostel.Event.Created;
using Hostel.Repository;
using Hostel.State.Floor.Units;
using Shared;
using Shared.Repository;

namespace Hostel.Entity.Handler.Floor.Units
{
    public class ToiletHandler : ICommandHandler<ToiletState>
    {
        public HandlerResult Handle(ToiletState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch (command)
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
