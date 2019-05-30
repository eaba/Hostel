using Hostel.Command;
using Hostel.Command.Create;
using Hostel.Event;
using Hostel.Event.Created;
using Hostel.Repository;
using Hostel.Repository.Write;
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
                
                case InstallSensor senors:
                    {
                        if (repository.InstallBathRoomSensors(state, out var newstate))
                        {
                            return new HandlerResult(new InstalledSensor(newstate.Sensors));
                        }
                        return new HandlerResult($"Sensors for {state.Tag} could not be installed at this time!");
                    }
                default: return HandlerResult.NotHandled(command);
            }
        }
    }
}
