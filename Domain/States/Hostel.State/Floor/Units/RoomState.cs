using Hostel.Model.Tenant;
using Shared;

namespace Hostel.State.Floor.Units
{
    public class RoomState: Message, IState<RoomState>
    {
        public string RoomId { get; }
        public string Tag { get; }
        public Tenant Tenant { get; }
        public RoomState(string id, string tag) : this(id, tag, null)
        {
            RoomId = id;
            Tag = tag;
        }
        public RoomState(string id, string tag, Tenant tenant)
        {
            RoomId = id;
            Tag = tag;
            Tenant = tenant;
        }
        public RoomState Update(IEvent evnt)
        {
            switch (evnt)
            {
                /*case CreatedRoom createdRoom:
                    {
                        var room = createdRoom.Room;
                        return new RoomState(room.RoomId, room.FloorId, room.Tag, false, Enumerable.Empty<Tenant>());
                    }
                case RentedOutRoom rentedOutRoom:
                    {
                        var tenant = rentedOutRoom.RentOut;
                        return new RoomState(RoomId, FloorId, Tag, true, tenant.Tenants);
                    }*/
                default:
                    return this;
            }
        }
    }
}
