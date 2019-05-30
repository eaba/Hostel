using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public sealed class CreateRoom : Message, ICommand
    {
        public readonly RoomSpecs Room;
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
        public CreateRoom(RoomSpecs room)
        {
            Room = room;
        }
        
    }
}
