using Hostel.Model;
using Hostel.State.Sensor;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hostel.State.Floor.Units
{
    public class ToiletState : Message, IState<ToiletState>
    {
        public string ToiletId { get; }
        public string Tag { get; }
        public IEnumerable<SensorSpec> Sensors { get; }
        public IEnumerable<Reading> Current { get; }
        public IEnumerable<Reading> Previous { get; }
        public ToiletState(string id, string tag, IEnumerable<SensorSpec> sensors) : this(id, tag, sensors, Enumerable.Empty<Reading>(), Enumerable.Empty<Reading>())
        {
            Tag = tag;
            Sensors = sensors;
            ToiletId = id;
        }
        public ToiletState(string id, string tag, IEnumerable<SensorSpec> sensors, IEnumerable<Reading> current, IEnumerable<Reading> previous)
        {
            Tag = tag;
            Sensors = sensors;
            ToiletId = id;
            Current = current;
            Previous = previous;
        }
        public ToiletState Update(IEvent evnt)
        {
            switch (evnt)
            {
                default: return this;
            }
        }
    }
}
