using Shared;

namespace Hostel.Event
{
    public class SensorInstalled : Message, IEvent
    {
        public Sensor.Model.Sensor Device { get; }
        public SensorInstalled(Sensor.Model.Sensor device)
        {
            Device = device;
        }
    }
}
