using Hostel.Command;
using Hostel.Event;
using Hostel.Repository;
using Hostel.Repository.Write;
using Hostel.State;
using Shared;
using Shared.Repository;

namespace Hostel.Entity.Handler
{
    public class SepticTankHandler : ICommandHandler<SepticTankState>
    {
        public HandlerResult Handle(SepticTankState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch(command)
            {
                case InstallSensor senors:
                    {
                        if(repository.InstallSepticTankSensors(state, out var newstate))
                        {
                            return new HandlerResult(new InstalledSensor(newstate.Sensors));
                        }
                        return new HandlerResult($"Sensors for {state.Type} could not be installed at this time!");
                    }
                default: return HandlerResult.NotHandled(command);
            }
        }
    }
}
