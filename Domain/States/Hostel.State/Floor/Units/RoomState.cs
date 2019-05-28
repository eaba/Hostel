﻿using Hostel.Model.Tenant;
using Shared;
using System.Collections.Immutable;

namespace Hostel.State.Floor.Units
{
    public class RoomState: Message, IState<RoomState>
    {
        public string RoomId { get; }
        public string Tag { get; }
        public Tenant Tenant { get; }
        public ImmutableDictionary<string, ICommand> PendingCommands { get; }
        public RoomState(string id, string tag) : this(id, tag, null, ImmutableDictionary<string, ICommand>.Empty)
        {
        }
        public RoomState(string id, string tag, Tenant tenant, ImmutableDictionary<string, ICommand> pendingCommands)
        {
            RoomId = id;
            Tag = tag;
            Tenant = tenant;
            PendingCommands = pendingCommands;
        }
        public RoomState Update(IEvent evnt)
        {
            switch (evnt)
            {
                default:
                    return this;
            }
        }
    }
}
