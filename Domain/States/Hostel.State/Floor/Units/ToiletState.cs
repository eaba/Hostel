using Hostel.Model;
using Hostel.State.Sensor;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        public ImmutableDictionary<string, ICommand> PendingCommands { get; }
        public ToiletState(string id, string tag, IEnumerable<SensorSpec> sensors) : this(id, tag, sensors, Enumerable.Empty<Reading>(), Enumerable.Empty<Reading>(), ImmutableDictionary<string, ICommand>.Empty)
        {
        }
        public ToiletState(string id, string tag, IEnumerable<SensorSpec> sensors, IEnumerable<Reading> current, IEnumerable<Reading> previous, ImmutableDictionary<string, ICommand> pendingCommands)
        {
            Tag = tag;
            Sensors = sensors;
            ToiletId = id;
            Current = current;
            Previous = previous;
            PendingCommands = pendingCommands;
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
