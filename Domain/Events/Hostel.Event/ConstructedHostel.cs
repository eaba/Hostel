using Hostel.Model;
using Shared;

namespace Hostel.Event
{
    public class ConstructedHostel : IEvent
    {
        public string ReplyToQueue { get; }
        public string Commander { get; }
        public string CommandId { get; }
        public Construction Construction { get; }
        public ConstructedHostel(Construction detail)
        {
            Construction = detail;
        }
    }
}
