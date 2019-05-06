using Hostel.Event;
using Hostel.State.Floor.Units;
using Shared;
using System;
using System.Collections.Generic;

namespace Hostel.State.Floor
{
    public class FloorState: Message, IState<FloorState>
    {
        public Guid FloorId { get; }
        public string Tag { get; }
        public IEnumerable<RoomState> RoomState;
        public static readonly FloorState Empty = new FloorState(Guid.Empty);
        public FloorState(Guid floorId, string tag)
        {
            FloorId = floorId;
            Tag = tag;
        }
        public FloorState(Guid floorId):this(floorId, string.Empty)
        {
        }
        public FloorState Update(IEvent evnt)
        {
            if(evnt is CreatedFloor createdFloor)
            {
                var floor = createdFloor.Floor;
                return new FloorState(floor.FloorId, floor.Tag);
            }
            return this;
        }
    }
}
