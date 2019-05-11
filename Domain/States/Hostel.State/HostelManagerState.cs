using Hostel.Event;
using Hostel.Model;
using Shared;

namespace Hostel.State
{
    public class HostelManagerState : Message, IState<HostelManagerState>
    {
        public Construction ConstructionRecord { get; }
        public bool Constructed;
        public HostelManagerState(bool constructed, Construction record)
        {
            Constructed = false;
            ConstructionRecord = record;
        }
        public HostelManagerState Update(IEvent evnt)
        {
            switch(evnt)
            {
                case ConstructedHostel hostel:
                    {
                        var construct = hostel.Construction;
                        var constructed = !string.IsNullOrWhiteSpace(hostel.Construction.Detail.HostelId);
                        return new HostelManagerState(constructed, construct);
                    }
                default: return this;
            }
        }
        
    }
}
