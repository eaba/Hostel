using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hostel.Command
{
    public class CreateWaterReservoir : Message, ICommand
    {
        public string Commander => throw new NotImplementedException();

        public string CommandId => throw new NotImplementedException();
    }
}
