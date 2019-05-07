using Hostel.Model;
using Shared;

namespace Hostel.Command
{
    public class CreateRoom : Message, ICommand
    {
        public readonly Room Room;
        public string Commander { get; }
        public string CommandId { get; }
        public CreateRoom(Room room, string commander, string commandid)
        {
            Room = room;
            Commander = commander;
            CommandId = commandid;
        }
        
    }
}
