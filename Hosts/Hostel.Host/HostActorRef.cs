using Akka.Actor;
using Shared;
using System.Collections.Concurrent;

namespace Hostel.Host
{
    public static class HostActorRef
    {
        public static IActorRef ActorRef;
        public static bool ActorIsReady = false;
        private static ConcurrentDictionary<string, IMassTransitCommand> CachedDtos = new ConcurrentDictionary<string, IMassTransitCommand>();
        internal static void CacheDto(IMassTransitCommand command)
        {
            if (CachedDtos.TryAdd(command.CommandId, command))
            {
                //PalmTree.ChurchOs.Services.Common.Log.LogError(JsonConvert.SerializeObject(dto, Formatting.Indented), "AddedToCache");
            }
        }
        internal static void ProcessCached()
        {
            foreach (var d in CachedDtos)
            {
                ActorRef.Tell(d.Value);
                CachedDtos.TryRemove(d.Key, out var dt);
            }
        }
    }
}
