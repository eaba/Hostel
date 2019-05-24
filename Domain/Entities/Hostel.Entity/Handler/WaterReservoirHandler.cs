using Hostel.Command;
using Hostel.Event;
using Hostel.Repository;
using Hostel.Repository.Write;
using Hostel.State;
using Shared;
using Shared.Repository;

namespace Hostel.Entity.Handler
{
    public class WaterReservoirHandler : ICommandHandler<WaterReservoirState>
    {
        public HandlerResult Handle(WaterReservoirState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch (command)
            {
                case InstallSensor senors:
                    {
                        if (repository.InstallReservoirSensors(state, out var newstate))
                        {
                            return new HandlerResult(new InstalledSensor(newstate.Sensors));
                        }
                        return new HandlerResult($"Sensors for {state.Type} could not be installed at this time!", string.Empty, string.Empty);
                    }
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
