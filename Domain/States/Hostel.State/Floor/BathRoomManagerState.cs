using Hostel.Model;
using Shared;
using System;
using System.Collections.Generic;

namespace Hostel.State.Floor
{
    public class BathRoomManagerState : Message, IState<BathRoomManagerState>
    {
        public string Tag { get; }
        public IEnumerable<BathRoomSpec> Specs { get; }
        public BathRoomManagerState(IEnumerable<BathRoomSpec> spec, string tag)
        {
            Tag = tag;
            Specs = spec;
        }

        public BathRoomManagerState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
