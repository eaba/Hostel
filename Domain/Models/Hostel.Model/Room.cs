using System;

namespace Hostel.Model
{
    public class Room
    {
        public Guid FloorId { get; }
        public Guid RoomId { get; }
        public string Tag { get; }
        public Room(Guid floor, Guid room, string tag)
        {
            FloorId = floor;
            RoomId = room;
            Tag = tag;
        }
    }
}
