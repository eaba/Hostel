using Hostel.Model;
using Shared;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
