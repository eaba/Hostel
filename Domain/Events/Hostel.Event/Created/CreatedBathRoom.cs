using Hostel.Model;
using Shared;

namespace Hostel.Event.Created
{
    public class CreatedBathRoom:IEvent
    {
        public readonly BathRoomSpec BathRoom;
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public CreatedBathRoom(BathRoomSpec bathRoom)
        {
            BathRoom = bathRoom;
        }
    }
}
