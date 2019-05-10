using Hostel.Model;
using Hostel.State.Sensor;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hostel.State.Floor.Units
{
    public class KitchenState : Message, IState<KitchenState>
    {
        public string KitchenId { get; }
        public string Tag { get; }
        public IEnumerable<SensorSpec> Sensors { get; }
        public IEnumerable<Reading> Current { get; }
        public IEnumerable<Reading> Previous { get; }
        public KitchenState(string id, string tag, IEnumerable<SensorSpec> sensors) : this(id,tag, sensors, Enumerable.Empty<Reading>(), Enumerable.Empty<Reading>())
        {
            KitchenId = id;
            Tag = tag;
            Sensors = sensors;
        }
        public KitchenState(string id, string tag, IEnumerable<SensorSpec> sensors, IEnumerable<Reading> current, IEnumerable<Reading> previous)
        {
            Tag = tag;
            Sensors = sensors;
            KitchenId = id;
            Current = current;
            Previous = previous;
        }
        public KitchenState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
