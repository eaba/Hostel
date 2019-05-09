using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hostel.State.Floor.Units
{
    public class KitchenState : Message, IState<KitchenState>
    {
        public KitchenState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
