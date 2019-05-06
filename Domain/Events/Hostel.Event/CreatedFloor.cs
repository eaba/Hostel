using Shared;

namespace Hostel.Event
{
    public class CreatedFloor:IEvent
    {
        public readonly Model.Floor Floor;
        public CreatedFloor(Model.Floor floor)
        {
            Floor = floor;
        }
    }
}
