using Hostel.Event;
using Hostel.Model;
using Shared;
using System.Collections.Immutable;

namespace Hostel.State
{
    public class HostelManagerState : Message, IState<HostelManagerState>
    {
        public readonly Construction ConstructionRecord;
        public ImmutableDictionary<string, ICommand> PendingCommands { get; }
        public ImmutableHashSet<IMassTransitEvent> PendingRsponses { get; }

        public readonly bool Constructed;
        public HostelManagerState(bool constructed, Construction record):this(constructed, record, ImmutableDictionary<string, ICommand>.Empty, ImmutableHashSet<IMassTransitEvent>.Empty)
        {
            Constructed = constructed;
        }
        public HostelManagerState(bool constructed, Construction record, ImmutableDictionary<string, ICommand> pendingCommands, ImmutableHashSet<IMassTransitEvent> pendingResponses)
        {
            Constructed = constructed;
            ConstructionRecord = record;
            PendingCommands = pendingCommands;
            PendingRsponses = pendingResponses;
        }
        public HostelManagerState Update(IEvent evnt)
        {
            switch(evnt)
            {
                case ConstructedHostel hostel:
                    {
                        var construct = hostel.Construction;
                        var constructed = !string.IsNullOrWhiteSpace(hostel.Construction.Detail.HostelId);
                        return new HostelManagerState(constructed, construct, PendingCommands, PendingRsponses);
                    }
                default: return this;
            }
        }
        
    }
}
