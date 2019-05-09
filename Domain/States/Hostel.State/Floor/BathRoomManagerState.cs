using Hostel.Model;
using Shared;
using System;
using System.Collections.Generic;

namespace Hostel.State.Floor
{
    public class BathRoomManagerState : Message, IState<BathRoomManagerState>
    {
        public string Tag { get; }
        public IEnumerable<BathRoomSpec> BathRooms { get; }
        public BathRoomManagerState(IEnumerable<BathRoomSpec> bathRooms, string tag)
        {
            Tag = tag;
            BathRooms = bathRooms;
        }

        public BathRoomManagerState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
