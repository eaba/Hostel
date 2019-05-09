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
        public IEnumerable<ToiletSpec> Toilets { get; }
        public ToiletManagerState(IEnumerable<ToiletSpec> toilets, string tag)
        {
            Tag = tag;
            Toilets = toilets;
        }
        public ToiletManagerState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
