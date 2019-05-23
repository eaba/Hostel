using Akka.Actor;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Akka.Extension
{
    public static class Extension
    {
        public static void AddServiceScopeFactory(this ActorSystem system, IServiceScopeFactory serviceFactory)
        {
            system.RegisterExtension(ServiceScopeExtensionIdProvider.Instance);
            ServiceScopeExtensionIdProvider.Instance.Get(system).Initialize(serviceFactory);
        }

        public static IServiceScope CreateScope(this IActorContext context)
        {
            return ServiceScopeExtensionIdProvider.Instance.Get(context.System).CreateScope();
        }
        
    }
}
