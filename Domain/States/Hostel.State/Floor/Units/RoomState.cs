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
                default:
                    return this;
            }
        }
    }
}
