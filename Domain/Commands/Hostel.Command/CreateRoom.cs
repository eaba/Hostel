using Hostel.Model;
using Shared;

namespace Hostel.Command
{
    public class CreateRoom : Message, ICommand
    {
        public readonly Room Room;
        public CreateRoom(Room room)
        {
            Room = room;
        }
    }
}
