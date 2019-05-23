using Akka.Actor;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Akka.Extension
{
    public class ServiceScopeExtension : IExtension
    {
        private IServiceScopeFactory _serviceProvider;

        public void Initialize(IServiceScopeFactory serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IServiceScope CreateScope()
        {
            return _serviceProvider.CreateScope();
        }
    }
}
