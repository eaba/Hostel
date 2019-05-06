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
        public FloorState(Guid floorId, string tag)
        {
            FloorId = floorId;
            Tag = tag;
        }
        public FloorState Update(IEvent evnt)
        {
            if(evnt is RoomRented roomRented)
            {
                var room = roomRented.Rented.Room;
                if(room.ContainsKey("Person") && room.ContainsKey("Room") && room.ContainsKey("Start") && room.ContainsKey("End"))
                {
                    OccupiedRooms.Add(room);
                    return new FloorState(FloorId, Tag, Rooms, OccupiedRooms);
                }
                
            }
            return this;
        }
    }
}
