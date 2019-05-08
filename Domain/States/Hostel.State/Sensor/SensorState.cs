using Shared;
using System;

namespace Hostel.State.Sensor
{
    public class SensorState : Message, IState<SensorState>
    {
        public SensorState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
