using Hostel.Command;
using Hostel.Event;
using Hostel.Repository;
using Hostel.State.Floor.Units;
using Shared;
using Shared.Repository;
using System;

namespace Hostel.Entity.Handler.Floor.Units
{
    public class KitchenHandler : ICommandHandler<KitchenState>
    {
        public HandlerResult Handle(KitchenState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch (command)
            {
                case InstallSensor senors:
                    {
                        if (repository.InstallSepticTankSensors(state, out var newstate))
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
