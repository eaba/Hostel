
using System.Collections.Generic;

namespace Shared
{
    public interface IMassTransitEvent
    {
        string Event { get; }
        string Commander { get; }
        string CommandId { get; }
        Dictionary<string, string> Payload { get; }
    }
}
