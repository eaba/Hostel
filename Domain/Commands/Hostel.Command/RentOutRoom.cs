
using Hostel.Model;
using Shared;

namespace Hostel.Command
{
    public class RentOutRoom : Message, ICommand
    {
        public readonly RentOut RentOut;
        public RentOutRoom(RentOut rentOut)
        {
            RentOut = rentOut;
        }
    }
}
