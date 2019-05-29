using Shared;
using System;
using System.Collections.Generic;

namespace Hostel.Event.Created
{
    public sealed class PersonCreated: IEvent
    {
        public Dictionary<string, string> Payload { get; }
        public PersonCreated(Dictionary<string, string> payload)
        {
            Payload = payload;
        }
    }
}
