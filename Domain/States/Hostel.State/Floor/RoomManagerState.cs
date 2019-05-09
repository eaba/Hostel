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
        public IEnumerable<RoomSpecs> Specs { get; }
        public RoomManagerState(IEnumerable<RoomSpecs> spec, string tag)
        {
            Tag = tag;
            Specs = spec;
        }
        public RoomManagerState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
