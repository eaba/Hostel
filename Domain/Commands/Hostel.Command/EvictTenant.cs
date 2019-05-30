using Shared;

namespace Hostel.Command
{
    public sealed class EvictTenant : Message, ICommand
    {
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
    }
}
