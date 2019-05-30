using Hostel.Event;
using Hostel.Event.Created;
using Hostel.Model;
using Hostel.State.Sensor;
using Shared;
using System.Collections.Generic;
using System.Collections.Immutable;

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
        public ImmutableDictionary<string, ICommand> PendingCommands { get; }
        public ImmutableHashSet<IMassTransitEvent> PendingResponses { get; }

        public WaterReservoirState(string id, int height, int alert, IEnumerable<SensorSpec> sensors) : this(id, height, alert, sensors, null, null, ImmutableDictionary<string, ICommand>.Empty, ImmutableHashSet<IMassTransitEvent>.Empty)
        {
        }
        public WaterReservoirState(string id, int height, int alert, IEnumerable<SensorSpec> sensors, Reading current, Reading previous, ImmutableDictionary<string, ICommand> pendingCommands, ImmutableHashSet<IMassTransitEvent> pendingResponses)
        {
            Height = height;
            AlertHeight = alert;
            Sensors = sensors;
            Previous = previous;
            Current = current;
            ReservoirId = id;
            PendingCommands = pendingCommands;
            PendingResponses = pendingResponses;
        }
        public WaterReservoirState Update(IEvent evnt)
        {
            switch (evnt)
            {
                case InstalledSensor sensor:
                    {
                        return new WaterReservoirState(ReservoirId, Height, AlertHeight, sensor.Sensors, Current, Previous, PendingCommands, PendingResponses);
                    }
                case CreatedWaterReservoir reservoir:
                    {
                        var spec = reservoir.ReservoirSpec;
                        return new WaterReservoirState(spec.ReservoirId, spec.Height, spec.AlertHeight, spec.Sensors, Current, Previous, PendingCommands, PendingResponses);
                    }
                default: return this;
            }
        }
    }
}
