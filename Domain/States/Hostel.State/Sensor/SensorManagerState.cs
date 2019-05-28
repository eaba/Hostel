using Shared;
using System;
using System.Collections.Immutable;

namespace Hostel.State.Sensor
{
    public class SensorManagerState : Message, IState<SensorManagerState>
    {
        public ImmutableDictionary<string, ICommand> PendingCommands { get; }
        public SensorManagerState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
