
using System.Collections.Generic;

namespace Shared
{
    public interface IMassTransitEvent
    {
        bool Success { get; }
        string Event { get; }
        string Commander { get; }
        string CommandId { get; }
        string Error { get; }
        Dictionary<string, string> Payload { get; }
        //void EventPayload(HandlerResult result);
    }
}
