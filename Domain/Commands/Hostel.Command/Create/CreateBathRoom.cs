using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public sealed class CreateBathRoom : Message, ICommand
    {
        public readonly BathRoomSpec BathRoom;
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
        public CreateBathRoom(BathRoomSpec bathRoom)
        {
            BathRoom = bathRoom;
        }
    }
}
