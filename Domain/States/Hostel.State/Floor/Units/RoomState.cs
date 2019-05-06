using Hostel.Model.Tenant;
using Shared;
using System;
using System.Collections.Generic;

namespace Hostel.State.Floor.Units
{
    public class RoomState: Message, IState<RoomState>
    {
        public Guid RoomId { get; }
        public Guid FloorId { get; }
        public string Tag { get; }
        public IEnumerable<Tenant> Occupants { get; }
        public RoomState(Guid room, Guid floor, string tag, IEnumerable<Tenant> occupants)
        {
            RoomId = room;
            FloorId = floor;
            Tag = tag;
            Occupants = occupants;
        }
        public RoomState Update(IEvent evnt)
        {

        }
    }
}
