using Hostel.Model;
using Shared;

namespace Hostel.Command
{
    public class CreateFloor: Message, ICommand
    {
        public readonly FloorSpec Floor;
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public CreateFloor(FloorSpec floor)
        {
            Floor = floor;
        }

    }
}
