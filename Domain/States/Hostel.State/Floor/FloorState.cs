using Hostel.Event.Created;
using Hostel.Model;
using Shared;
using System.Collections.Immutable;

namespace Hostel.State.Floor
{
    public class FloorState: Message, IState<FloorState>
    {
        public FloorSpec FloorSpec { get; }
        public ImmutableDictionary<string, ICommand> PendingCommands { get; }
        public FloorState(FloorSpec spec):this(spec, ImmutableDictionary<string, ICommand>.Empty)
        {
        }
        public FloorState(FloorSpec spec, ImmutableDictionary<string, ICommand> pendingCommands)
        {
            FloorSpec = spec;
            PendingCommands = pendingCommands;
        }

        public FloorState Update(IEvent evnt)
        {
            switch (evnt)
            {
                case CreatedFloor createdFloor:
                    {
                        var floor = createdFloor.Floor;
                        return new FloorState(floor, PendingCommands);
                    }
                default: return this;
            }
        }
    }
}
