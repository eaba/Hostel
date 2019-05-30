
using Shared;

namespace Hostel.Command.StateChange.Floor.Units
{
    public class StoreRoomStateChange : Message, ICommand
    {
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
    }
}
