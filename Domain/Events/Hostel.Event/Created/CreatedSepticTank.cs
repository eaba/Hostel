using Hostel.Model;
using Shared;

namespace Hostel.Event.Created
{
    public class CreatedSepticTank : IEvent
    {
        public SepticTankSpec SepticTankSpec { get; }
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public CreatedSepticTank(SepticTankSpec spec)
        {
            SepticTankSpec = spec;
        }
    }
}
