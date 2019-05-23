using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MassTransit.Event
{
    public sealed class MassTransitEvent : IMassTransitEvent
    {
        public string Event { get; }

        public string Commander { get; }

        public string CommandId { get; }

        public Dictionary<string, string> Payload { get; }
        public MassTransitEvent(string evnt, string commander, string commandid, Dictionary<string, string> payload)
        {
            Event = evnt;
            Commander = commander;
            CommandId = commandid;
            Payload = payload;
        }
    }
}
