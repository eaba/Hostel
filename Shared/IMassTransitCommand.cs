using System;
using System.Collections.Generic;

namespace Shared
{
    public interface IMassTransitCommand
    {
        string Command { get; }
        string Commander { get; }
        string CommandId { get; }
        string ReplyToQueue { get; }
        Dictionary<string, string> Payload { get; }
    }
}
