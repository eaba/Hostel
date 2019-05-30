using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public sealed class CreateFloor: Message, ICommand
    {
        public readonly FloorSpec Floor;
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public string ReplyToQueue { get; }

        public CreateFloor(FloorSpec floor)
        {
            Floor = floor;
        }
    }
}
