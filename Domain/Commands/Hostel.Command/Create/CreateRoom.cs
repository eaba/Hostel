using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public class CreateRoom : Message, ICommand
    {
        public readonly RoomSpec Room;
        public string Commander { get; }
        public string CommandId { get; }
        public CreateRoom(RoomSpec room)
        {
            Room = room;
        }
        
    }
}
