using Shared;
using System;

namespace Hostel.State.Sensor
{
    public class SensorManagerState : Message, IState<SensorManagerState>
    {
        public SensorManagerState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
