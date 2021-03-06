﻿using System.Collections.Immutable;

namespace Shared
{
    public interface IState<out TState>
    {
        ImmutableHashSet<IMassTransitEvent> PendingResponses { get; }
        ImmutableDictionary<string, ICommand> PendingCommands { get; } //To hold records of all non-executed commands so that we can retry should the Actor die - Maybe of heart-attack or high-blood pressure!!! ;)
        TState Update(IEvent evnt);
    }
}
