using Shared;
using System;

namespace Hostel.State.Sensor
{
    public class SensorState : Message, IState<SensorState>
    {
        public Guid SensorId { get; }
        public Guid BathRoom { get; }
        public string Tag { get; }
        public string Role { get; }
        public Reading Current { get; }
        public Reading Previous { get; }
        public static readonly SensorState Empty = new SensorState();
        public SensorState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
    public class Reading
    {
        public string Value { get; }
        public string TimeStamp { get; }
        public Reading(string value, string time)
        {
            Value = value;
            TimeStamp = time;
        }
    }
}
