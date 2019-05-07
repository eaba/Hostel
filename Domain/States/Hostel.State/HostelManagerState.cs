using Hostel.Event;
using Hostel.State.Floor;
using Shared;
using System.Collections.Generic;
using System.Linq;

namespace Hostel.State
{
    public class HostelManagerState : Message, IState<HostelManagerState>
    {
        public IEnumerable<FloorState> FloorStates { get; }
        public SepticTankState SepticTankState { get; }
        public WaterReservoirState WaterReservoirState { get; }
        public bool Constructed;
        public static readonly HostelManagerState Empty = new HostelManagerState(Enumerable.Empty<FloorState>(), false);
        public HostelManagerState(IEnumerable<FloorState> floors, bool constructed)
        {
            FloorStates = floors;
            Constructed = false;
        }
        public HostelManagerState Update(IEvent evnt)
        {
            switch(evnt)
            {
                case CreatedFloor createdFloor:
                    {
                        var floor = createdFloor.Floor;
                        return new HostelManagerState(FloorStates, true);
                    }
                default: return this;
            }
        }
        
    }
}
