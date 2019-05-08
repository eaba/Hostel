using Shared;
using static Hostel.Model.Construction;

namespace Hostel.Event
{
    public class ConstructedHostel : IEvent
    {
        public string Commander => string.Empty;

        public string CommandId => string.Empty;
        public HostelDetail Detail { get; }
        public ConstructedHostel(HostelDetail detail)
        {
            Detail = detail;
        }
    }
}
