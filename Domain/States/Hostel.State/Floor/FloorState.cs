using Hostel.Model;
using Shared;

namespace Hostel.State.Floor
{
    public class FloorState: Message, IState<FloorState>
    {
        public FloorSpec FloorSpec { get; }
        public FloorState(FloorSpec spec)
        {
            FloorSpec = spec;
        }
        
        public FloorState Update(IEvent evnt)
        {            
            return this;
        }
    }
}
