using Hostel.Model;
using Shared;
using System;

namespace Hostel.Event
{
    public class RoomRented: Message, IEvent
    {
        public Rented Rented { get; }
        public RoomRented(Rented rented)
        {
            Rented = rented;
        }
    }
}
