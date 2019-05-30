using Shared;
using System;
using System.Collections.Immutable;

namespace Hostel.State.Sensor
{
    public class SensorState : Message, IState<SensorState>
    {
        public string SensorId { get; }
        public string Tag { get; }
        public string Role { get; }
        public Reading Current { get; }
        public Reading Previous { get; }
        public ImmutableDictionary<string, ICommand> PendingCommands { get; }
        public ImmutableHashSet<IMassTransitEvent> PendingResponses { get; }

        public SensorState(string sensorid, string tag, string role):this(sensorid, tag, role, null, null, ImmutableDictionary<string, ICommand>.Empty, ImmutableHashSet <IMassTransitEvent>.Empty)
        {
        }
        public SensorState(string sensorid, string tag, string role, Reading current, Reading previous, ImmutableDictionary<string, ICommand> pendingCommands, ImmutableHashSet<IMassTransitEvent> pendingResponses)
        {
            SensorId = sensorid;
            Tag = tag;
            Role = role;
            Current = current;
            Previous = previous;
            PendingCommands = pendingCommands;
            PendingResponses = pendingResponses;
        }
        public SensorState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
    public class Reading
    {
        public string Role { get; }
        public string Value { get; }
        public string TimeStamp { get; }
        public Reading(string value, string time, string role = "")
        {
            Value = value;
            TimeStamp = time;
            Role = role;
        }
    }
}
