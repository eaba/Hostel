using Hostel.Model;
using Shared;

namespace Hostel.Event
{
    public class CreatedRoom:IEvent
    {
        public readonly Room Room;
        public string Commander { get; }
        public string CommandId { get; }
        public CreatedRoom(Room room, string commander, string commandid)
        {
            Room = room;
            Commander = commander;
            CommandId = commandid;
        }        
    }
}
