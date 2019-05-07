using Hostel.Event;
using Hostel.State.Floor;
using Shared;
using System.Collections.Generic;
using System.Linq;

namespace Hostel.State
{
    public class HostelManagerState : Message, IState<HostelManagerState>
    {
        public IEnumerable<FloorState> FloorStates;
        public static readonly HostelManagerState Empty = new HostelManagerState(Enumerable.Empty<FloorState>());
        public HostelManagerState(IEnumerable<FloorState> floors)
        {
            FloorStates = floors;
        }
        public HostelManagerState Update(IEvent evnt)
        {
            switch(evnt)
            {
                case CreatedFloor createdFloor:
                    {
                        var floor = createdFloor.Floor;
                        return new HostelManagerState(FloorStates);
                    }
                default: return this;
            }
        }
        
    }
}
