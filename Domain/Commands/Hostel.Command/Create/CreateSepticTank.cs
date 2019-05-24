using Hostel.Model;
using Shared;
using System.Collections.Generic;

namespace Hostel.Command.Create
{
    public sealed class CreateSepticTank : Message, ICommand
    {
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public SepticTankSpec Spec { get; }
        public CreateSepticTank(SepticTankSpec spec)
        {
            Spec = spec;
        }
    }
}
