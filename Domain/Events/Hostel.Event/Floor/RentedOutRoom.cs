
using Hostel.Model;
using Shared;

namespace Hostel.Event.Floor
{
    public class RentedOutRoom: IEvent
    {
        public readonly RentOut RentOut;
        public RentedOutRoom(RentOut rentOut)
        {
            RentOut = rentOut;
        }
    }
}
