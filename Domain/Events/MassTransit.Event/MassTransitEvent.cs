﻿using Hostel.Event.Created;
using Shared;
using System;
using System.Collections.Generic;

namespace MassTransit.Event
{
    public sealed class MassTransitEvent : IMassTransitEvent
    {

        public bool Success { get; }
        public string Error { get; }
        public string Event { get; }
        public string Commander { get; }
        public string CommandId { get; }

        public Dictionary<string, string> Payload { get; }

        public MassTransitEvent(string commander, string commandid, HandlerResult result)
        {
            Event = result.Event.GetType().ToString();
            Commander = commander;
            CommandId = commandid;
            Success = result.Success;
            Error = string.Join(Environment.NewLine, result.Errors);
            switch (result.Event)
            {
                case PersonCreated person:
                    {
                        Payload = person.Payload;
                    }
                    break;
            }
        }

    }
}
