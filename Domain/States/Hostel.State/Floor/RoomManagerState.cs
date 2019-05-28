using Hostel.Event.Created;
using Hostel.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Hostel.State.Floor
{
    public class RoomManagerState : Message, IState<RoomManagerState>
    {
        public string FloorId { get; }
        public string Tag { get; }
        public IEnumerable<RoomSpecs> Rooms { get; }
        public ImmutableDictionary<string, ICommand> PendingCommands { get; }
        public RoomManagerState(string floor, IEnumerable<RoomSpecs> rooms, string tag):this(floor, rooms, tag, ImmutableDictionary<string, ICommand>.Empty)
        {
        }
        public RoomManagerState(string floor, IEnumerable<RoomSpecs> rooms, string tag, ImmutableDictionary<string, ICommand> pendingCommands)
        {
            FloorId = floor;
            Tag = tag;
            Rooms = rooms;
            PendingCommands = pendingCommands;
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
                        return new RoomManagerState(FloorId, rooms, room.Tag, PendingCommands);
                    }
                default: return this;
            }
        }
    }
}
