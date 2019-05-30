using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public sealed class CreateFloor: Message, ICommand
    {
        public readonly FloorSpec Floor;
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }

        public CreateFloor(FloorSpec floor, string commander, string commandid)
        {
            Floor = floor;
            Commander = commander;
            CommandId = commandid;
        }
    }
}
