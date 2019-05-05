using Shared;
using System;

namespace Hostel.State
{
    public class RoomState: Message, IState<RoomState>
    {
        public Guid RoomId { get; }
        public Guid FloorId { get; }
        public string Tag { get; }
        public Guid PersonId { get; }
        public DateTime TenancyStart { get; }
        public DateTime TenancyEnd { get; }
        public RoomState Update(IEvent evnt)
        {

        }
    }
}
