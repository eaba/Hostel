using System.Collections.Generic;
using System.Linq;

namespace Shared
{
    public class HandlerResult : Message
    {
        public bool Success { get; }
        public bool WasHandled { get; }
        public IEvent Event { get; }
        public IEnumerable<string> Errors { get; }
        public string Commander { get; }
        public string CommandId { get; }

        public HandlerResult(string error, string commander, string commandid)
            : this(new[] { error }, commander, commandid)
        {

        }

        public HandlerResult(IEnumerable<string> errors, string commander, string commandid)
        {
            Success = false;
            Errors = errors;
            Event = null;
            WasHandled = true;
            Commander = commander;
            CommandId = commandid;
        }

        public HandlerResult(IEvent evnt): this(Enumerable.Empty<string>(), string.Empty, string.Empty)
        {
            Event = evnt;
            Success = true;
        }

        public static HandlerResult NotHandled(object command, string commander, string commandid) =>
            new HandlerResult($"Event type {command.GetType().FullName} not supported by handler.", commander, commandid);
    }
}
