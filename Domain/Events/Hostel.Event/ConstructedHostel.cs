using Hostel.Model;
using Shared;

namespace Hostel.Event
{
    public class ConstructedHostel : IEvent
    {
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public Construction Construction { get; }
        public ConstructedHostel(Construction detail)
        {
            Construction = detail;
        }
    }
}
