using Hostel.Model;
using Hostel.State.Sensor;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hostel.State.Floor.Units
{
    public class BathRoomState : Message, IState<BathRoomState>
    {
        public string BathRoomId { get; }
        public IEnumerable<SensorSpec> Sensors { get; }
        public IEnumerable<Reading> Current { get; }
        public IEnumerable<Reading> Previous { get; }
        public BathRoomState(string id, IEnumerable<SensorSpec> sensors) : this(id, sensors, Enumerable.Empty<Reading>(), Enumerable.Empty<Reading>())
        {
            Sensors = sensors;
            BathRoomId = id;
        }
        public BathRoomState(string id, IEnumerable<SensorSpec> sensors, IEnumerable<Reading> Current, IEnumerable<Reading> previous)
        {
            Sensors = sensors;
        }

        public BathRoomState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
