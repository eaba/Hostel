using Hostel.Event;
using Hostel.Event.Floor;
using Hostel.Model.Tenant;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hostel.State.Floor.Units
{
    public class RoomState: Message, IState<RoomState>
    {
        public Guid RoomId { get; }
        public Guid FloorId { get; }
        public string Tag { get; }
        public bool IsTaken { get; }
        public IEnumerable<Tenant> Occupants { get; }
        public RoomState(Guid room, Guid floor, string tag, bool taken, IEnumerable<Tenant> occupants)
        {
            RoomId = room;
            FloorId = floor;
            Tag = tag;
            IsTaken = taken;
            Occupants = occupants;
        }
        public RoomState Update(IEvent evnt)
        {
            switch (evnt)
            {
                case CreatedRoom createdRoom:
                    {
                        var room = createdRoom.Room;
                        return new RoomState(room.RoomId, room.FloorId, room.Tag, false, Enumerable.Empty<Tenant>());
                    }
                case RentedOutRoom rentedOutRoom:
                    {
                        var tenant = rentedOutRoom.RentOut;
                        return new RoomState(RoomId, FloorId, Tag, true, tenant.Tenants);
                    }
                default:
                    return this;
            }
        }
    }
}
