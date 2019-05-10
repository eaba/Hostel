using Hostel.Event.Created;
using Hostel.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hostel.State.Floor
{
    public class RoomManagerState : Message, IState<RoomManagerState>
    {
        public string Tag { get; }
        public IEnumerable<RoomSpecs> Rooms { get; }
        public RoomManagerState(IEnumerable<RoomSpecs> rooms, string tag)
        {
            Tag = tag;
            Rooms = rooms;
        }
        public RoomManagerState Update(IEvent evnt)
        {
            switch (evnt)
            {
                case CreatedRoom createdRoom:
                    {
                        var room = createdRoom.Room;
                        var rooms = Rooms.Where(x => x.Tag != room.Tag).ToList();
                        rooms.Add(room);
                        return new RoomManagerState(rooms, room.Tag);
                    }
                default: return this;
            }
        }
    }
}
