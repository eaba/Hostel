using Hostel.Event.Created;
using Hostel.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Hostel.State.Floor
{
    public class BathRoomManagerState : Message, IState<BathRoomManagerState>
    {
        public string FloorId { get; }
        public string Tag { get; }
        public IEnumerable<BathRoomSpec> BathRooms { get; }
        public ImmutableDictionary<string, ICommand> PendingCommands { get; }
        public BathRoomManagerState(string floor, IEnumerable<BathRoomSpec> bathRooms, string tag):this(floor, bathRooms, tag, ImmutableDictionary<string, ICommand>.Empty)
        {
        }
        public BathRoomManagerState(string floor, IEnumerable<BathRoomSpec> bathRooms, string tag, ImmutableDictionary<string, ICommand> pendingCommands)
        {
            FloorId = floor;
            Tag = tag;
            BathRooms = bathRooms;
            PendingCommands = pendingCommands;
        }
        public BathRoomManagerState Update(IEvent evnt)
        {
            switch (evnt)
            {
                case CreatedBathRoom createdBathRoom:
                    {
                        var bathroom = createdBathRoom.BathRoom;
                        var bathrooms = BathRooms.Where(x => x.Tag != bathroom.Tag).ToList();
                        bathrooms.Add(bathroom);
                        return new BathRoomManagerState(FloorId, bathrooms, bathroom.Tag, PendingCommands);
                    }
                default: return this;
            }
        }
    }
}
