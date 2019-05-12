using Akka.Actor;
using Akka.Event;
using Akka.Extension;
using LogShared;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Akka.MassTransit.Logger
{
    public class TraceLogger : UntypedActor
    {
        private ISendEndpoint _sendEndPoint;
        private List<Dictionary<string, string>> _traceCache;
        private string _traceUri = "";
        public TraceLogger()
        {
            _traceCache = new List<Dictionary<string, string>>();
        }
        protected override void OnReceive(object message)
        {
            message.Match()
                 .With<InitializeLogger>(m => Sender.Tell(new LoggerInitialized()))
                 .With<Error>(m => Trace(m.ToString()))
                 .With<Warning>(m => Trace(m.ToString()))
                 .With<DeadLetter>(m => Trace(string.Format("Deadletter - unable to send message {0} from {1} to {2}", m.Message, m.Sender, m.Sender)))
                 .With<UnhandledMessage>(m => Trace("Unhandled message!"))
                 .Default(m =>
                 {
                     if (m != null)
                         Trace(m.ToString());
                 });
        }
        protected override void PreStart()
        {
            var akkaConfig = Context.System.Settings.Config.GetConfig("akka");
            _traceUri = akkaConfig.GetString("trace-uri");
            base.PreStart();
        }

        protected override void PostStop()
        {
            base.PostStop();
        }
        private void Trace(string trace)
        {
            var data = new Dictionary<string, string> { { "Trace", trace } };
            try
            {               
                var dto = new Dto(data.ToImmutableDictionary());
                if (_sendEndPoint != null)
                {
                    DeCache();
                    _sendEndPoint.Send(dto);
                }
                else
                {
                    var context = Context.CreateScope();
                    if (context != null)
                    {
                        using (IServiceScope serviceScope = context)
                        {
                            var busControl = serviceScope.ServiceProvider.GetService<IBusControl>();
                            _sendEndPoint = busControl.GetSendEndpoint(new Uri(_traceUri)).GetAwaiter().GetResult();
                            if (_sendEndPoint != null)
                            {
                                DeCache();
                                _sendEndPoint.Send(dto);
                            }
                            else
                            {
                                _traceCache.Add(data);
                            }
                        }
                    }
                    else
                    {
                        _traceCache.Add(data);
                    }
                }

            }
            catch (Exception e)
            {
                _traceCache.Add(data);
            }
        }
        private void DeCache()
        {
            if (_traceCache.Count > 0)
            {
                foreach (var log in _traceCache)
                {
                    _sendEndPoint.Send(new Dto(log.ToImmutableDictionary()));
                }
                _traceCache.Clear();
            }
        }
    }
}
