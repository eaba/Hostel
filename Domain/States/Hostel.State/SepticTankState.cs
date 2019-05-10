using Hostel.Event;
using Hostel.Event.Created;
using Hostel.Model;
using Hostel.State.Sensor;
using Shared;
using System.Collections.Generic;

namespace Hostel.State
{
    public class SepticTankState : Message, IState<SepticTankState>
    {
        public string SepticTankId { get; }
        public int Height { get; }
        public Reading Previous { get; }
        public Reading Current { get; }
        public int AlertHeight { get; }
        public IEnumerable<SensorSpec> Sensors { get; }
        public SepticTankState(string id, int height, int alert, IEnumerable<SensorSpec> sensors) :this(id, height, alert, sensors, null, null)
        {
            Height = height;
            AlertHeight = alert;
            Sensors = sensors;
            SepticTankId = id;
        }
        public SepticTankState(string id, int height, int alert, IEnumerable<SensorSpec> sensors, Reading current, Reading previous)
        {
            Height = height;
            AlertHeight = alert;
            Sensors = sensors;
            Previous = previous;
            Current = current;
            SepticTankId = id;
        }
        public SepticTankState Update(IEvent evnt)
        {
            switch(evnt)
            {
                case InstalledSensor sensor:
                    {
                        return new SepticTankState(SepticTankId, Height, AlertHeight, sensor.Sensors, Current, Previous);
                    }
                case CreatedSepticTank tank:
                    {
                        var spec = tank.SepticTankSpec;
                        return new SepticTankState(spec.SepticTankId, spec.Height, spec.AlertHeight, spec.Sensors);
                    }
                default: return this;
            }
        }
    }
}
