using Hostel.Model;
using Shared;
using System.Collections.Generic;

namespace Hostel.Event
{
    public class InstalledSensor : IEvent
    {
        public string Commander { get; }
        public string CommandId { get; }
        public IEnumerable<SensorSpec> Sensors { get; }
        public InstalledSensor(IEnumerable<SensorSpec> sensors)
        {
            Sensors = sensors;
        }
    }
}
