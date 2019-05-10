
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

        public string Commander => throw new System.NotImplementedException();

        public string CommandId => throw new System.NotImplementedException();
    }
}
