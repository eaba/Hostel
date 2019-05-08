using Hostel.Model;
using Shared;

namespace Hostel.Command
{
    public class CreateWaterReservoir : Message, ICommand
    {
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public ReservoirSpec Spec { get; }
        public CreateWaterReservoir(ReservoirSpec spec)
        {
            Spec = spec;
        }
    }
}
