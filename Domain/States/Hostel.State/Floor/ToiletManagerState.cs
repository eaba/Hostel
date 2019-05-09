using Hostel.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hostel.State.Floor
{
    public class ToiletManagerState : Message, IState<ToiletManagerState>
    {
        public string Tag { get; }
        public IEnumerable<ToiletSpec> Specs { get; }
        public ToiletManagerState(IEnumerable<ToiletSpec> spec, string tag)
        {
            Tag = tag;
            Specs = spec;
        }
        public ToiletManagerState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
