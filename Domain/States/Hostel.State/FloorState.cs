using Hostel.Event;
using Shared;
using System;
using System.Collections.Generic;

namespace Hostel.State
{
    public class FloorState: Message, IState<FloorState>
    {
        public Guid FloorId { get; }
        public string Tag { get; }
        public int Rooms { get; }
        public List<Dictionary<string, string>> OccupiedRooms;
        public FloorState(Guid floorId, string tag, int rooms, List<Dictionary<string, string>> occupied)
        {
            FloorId = floorId;
            Tag = tag;
            Rooms = rooms;
            OccupiedRooms = occupied;
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
