using Hostel.Model;
using Shared;

namespace Hostel.Event.Created
{
    public class CreatedRoom:IEvent
    {
        public RoomSpecs Room { get; }
        public string Commander { get; }
        public string CommandId { get; }
        public CreatedRoom(RoomSpecs room)
        {
            Room = room;
        }        
    }
}
