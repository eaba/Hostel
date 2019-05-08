using Hostel.State.Sensor;
using Shared;
using Shared.Repository;

namespace Hostel.Entity.Handler.Sensor
{
    public class SensorHandler : ICommandHandler<SensorState>
    {
        public HandlerResult Handle(SensorState state, ICommand command, IRepository<IDbProperties> repository)
        {
            throw new System.NotImplementedException();
        }
    }
}
