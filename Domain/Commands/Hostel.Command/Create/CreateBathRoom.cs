using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public sealed class CreateBathRoom : Message, ICommand
    {
        public readonly BathRoomSpec BathRoom;
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public CreateBathRoom(BathRoomSpec bathRoom)
        {
            BathRoom = bathRoom;
        }
    }
}
