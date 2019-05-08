using Hostel.Model;
using Shared;

namespace Hostel.Event
{
    public class CreatedWaterReservoir : IEvent
    {
        public ReservoirSpec ReservoirSpec { get; }
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public CreatedWaterReservoir(ReservoirSpec spec)
        {
            ReservoirSpec = spec;
        }
    }
}
