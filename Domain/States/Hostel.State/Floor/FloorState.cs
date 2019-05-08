using Hostel.Event;
using Hostel.Model;
using Shared;
using System;

namespace Hostel.State.Floor
{
    public class FloorState: Message, IState<FloorState>
    {
        public FloorSpec FloorSpec { get; }
        public Guid FloorId { get; }
        public string Tag { get; }
        public bool Alive { get; }
        //public IEnumerable<RoomState> RoomState;
        public static readonly FloorState Empty = new FloorState();
        public FloorState()
        {
        }
        private FloorState(FloorSpec floorSpec, Guid floorId, string tag, bool isalive)
        {
            FloorSpec = floorSpec;
            FloorId = floorId;
            Tag = tag;
            Alive = isalive;
        }
        public FloorState Update(IEvent evnt)
        {
            if(evnt is CreatedFloor createdFloor)
            {
                var floor = createdFloor.Floor;
                return new FloorState(floor, Guid.Parse(floor.FloorId), floor.Tag, string.IsNullOrWhiteSpace(floor.FloorId)?false:true);
            }
            return this;
        }
    }
}
