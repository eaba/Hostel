using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer.Host
{
    public class IdentityState : Message, IState<IdentityState>
    {
        public IdentityState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
