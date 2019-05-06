using Hostel.Model;
using Shared;

namespace Hostel.Command
{
    public class CreateFloor: Message, ICommand
    {
        public readonly Floor Floor;
        public CreateFloor(Floor floor)
        {
            Floor = floor;
        }
    }
}
