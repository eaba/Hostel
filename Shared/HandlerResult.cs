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

        public HandlerResult(string error)
            : this(new[] { error })
        {

        }

        public HandlerResult(IEnumerable<string> errors)
        {
            Success = false;
            Errors = errors;
            Event = null;
            WasHandled = true;
        }

        public HandlerResult(IEvent evnt)
            : this(Enumerable.Empty<string>())
        {
            Event = evnt;
            Success = true;
        }

        public static HandlerResult NotHandled(object command) =>
            new HandlerResult(
                $"Event type {command.GetType().FullName} not supported by handler.");
    }
}
