
using Hostel.State.Floor;
using Shared;

namespace Hostel.Command.StateChange.Floor
{
    public class StoreFloorStateChange : Message, ICommand
    {
        public readonly FloorState State;
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
        public StoreFloorStateChange(FloorState state, string commander, string commandid)
        {
            Commander = commander;
            CommandId = commandid;
            State = state;
        }
    }
}
