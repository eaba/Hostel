using Hostel.Model;
using Shared;

namespace Hostel.Event.Created
{
    public class CreatedFloor:IEvent
    {
        public readonly FloorSpec Floor;
        public string Commander { get; }
        public string CommandId { get; }
        public CreatedFloor(FloorSpec floor)
        {
            Floor = floor;
        }        
    }
}
