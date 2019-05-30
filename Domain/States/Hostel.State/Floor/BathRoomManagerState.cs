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
        public ImmutableHashSet<IMassTransitEvent> PendingResponses { get; }

        public BathRoomManagerState(string floor, IEnumerable<BathRoomSpec> bathRooms, string tag):this(floor, bathRooms, tag, ImmutableDictionary<string, ICommand>.Empty, ImmutableHashSet<IMassTransitEvent>.Empty)
        {
        }
        public BathRoomManagerState(string floor, IEnumerable<BathRoomSpec> bathRooms, string tag, ImmutableDictionary<string, ICommand> pendingCommands, ImmutableHashSet<IMassTransitEvent> pendingResponses)
        {
            FloorId = floor;
            Tag = tag;
            BathRooms = bathRooms;
            PendingCommands = pendingCommands;
            PendingResponses = pendingResponses;
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
                        return new BathRoomManagerState(FloorId, bathrooms, bathroom.Tag, PendingCommands, PendingResponses);
                    }
                default: return this;
            }
        }
    }
}
