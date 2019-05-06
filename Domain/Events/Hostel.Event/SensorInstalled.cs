using Shared;

namespace Hostel.Event
{
    public class SensorInstalled : IEvent
    {
        public Sensor.Model.Sensor Device { get; }
        public SensorInstalled(Sensor.Model.Sensor device)
        {
            Device = device;
        }
    }
}
