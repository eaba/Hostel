using Hostel.Model;
using Shared;

namespace Hostel.Command
{
    public class CreateFloor: Message, ICommand
    {
        public readonly Floor Floor;
        public string Commander { get; }
        public string CommandId { get; }
        public CreateFloor(Floor floor, string commander, string commandid)
        {
            Floor = floor;
            Commander = commander;
            CommandId = commandid;
        }

    }
}
