using Hostel.Event;
using Hostel.Model;
using Shared;
using System.Collections.Generic;

namespace Hostel.State
{
    public class HostelManagerState : Message, IState<HostelManagerState>
    {
        public Construction ConstructionRecord { get; }
        public Dictionary<string, ICommand> PendingCommands { get; } //To hold records of all non-executed commands so that we can retry should the Actor die - Maybe of heart-attack or high-blood pressure!!! ;)
        public bool Constructed;
        public HostelManagerState(bool constructed, Construction record)
        {
            Constructed = false;
            ConstructionRecord = record;
            PendingCommands = new Dictionary<string, ICommand>();
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
