using Hostel.Model;
using Shared;

namespace Hostel.Event
{
    public class CreatedRoom:IEvent
    {
        public readonly Room Room;
        public CreatedRoom(Room room)
        {
            Room = room;
        }
    }
}
