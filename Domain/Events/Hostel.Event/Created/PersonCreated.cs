using Shared;
using System;
using System.Collections.Generic;

namespace Hostel.Event.Created
{
    public sealed class PersonCreated: IEvent
    {
        public string Commander { get; }
        public string CommandId { get; }
        public Dictionary<string, string> Payload { get; }
        public PersonCreated(string commander, string commandid, Dictionary<string, string> payload)
        {
            Commander = commander;
            CommandId = commandid;
            Payload = payload;
        }
    }
}
