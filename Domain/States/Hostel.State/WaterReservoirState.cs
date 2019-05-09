using Hostel.Model;
using Hostel.State.Sensor;
using Shared;
using System.Collections.Generic;

namespace Hostel.State
{
    public class WaterReservoirState : Message, IState<WaterReservoirState>
    {
        public string ReservoirId { get; }
        public int Height { get; }
        public Reading Previous { get; }
        public Reading Current { get; }
        public int AlertHeight { get; }
        public IEnumerable<SensorSpec> Sensors { get; }
        public WaterReservoirState(string id, int height, int alert, IEnumerable<SensorSpec> sensors) : this(id, height, alert, sensors, null, null)
        {
            Height = height;
            AlertHeight = alert;
            Sensors = sensors;
            ReservoirId = id;
        }
        public WaterReservoirState(string id, int height, int alert, IEnumerable<SensorSpec> sensors, Reading current, Reading previous)
        {
            Height = height;
            AlertHeight = alert;
            Sensors = sensors;
            Previous = previous;
            Current = current;
            ReservoirId = id;
        }
        public WaterReservoirState Update(IEvent evnt)
        {
            throw new System.NotImplementedException();
        }
    }
}
