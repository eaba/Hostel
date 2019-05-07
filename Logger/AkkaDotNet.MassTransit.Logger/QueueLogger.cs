using MassTransit;
using System;
using Akka.Actor;
using Akka.Event;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Immutable;
namespace Akka.MassTransit.Logger
{
    public class QueueLogger : ReceiveActor
    {
        private ISendEndpoint _sendEndPoint;
        private List<Dictionary<string, string>> _logCache;
        private string _queueUri = "";
        public QueueLogger()
        {
            _logCache = new List<Dictionary<string, string>>();
            ReceiveAsync<Debug>(async e => await LogAsync(e.LogLevel().ToString(), e.ToString(), Context.System.Name));
            ReceiveAsync<Info>(async e => await LogAsync(e.LogLevel().ToString(), e.ToString(), Context.System.Name));
            ReceiveAsync<Warning>(async e => await LogAsync(e.LogLevel().ToString(), e.ToString(), Context.System.Name));
            ReceiveAsync<Error>(async e => await LogAsync(e.LogLevel().ToString(), e.ToString(), Context.System.Name));
            Receive<InitializeLogger>(_=> Init(Sender));
        }
        protected override void PreStart()
        {
            var akkaConfig = Context.System.Settings.Config.GetConfig("akka");
            _queueUri = akkaConfig.GetString("queue-uri");
            base.PreStart();
        }

        protected override void PostStop()
        {
            base.PostStop();
        }
        private async Task LogAsync(string level, string message, string system)
        {
            try
            {
                var data = new Dictionary<string, string> {
                    {"Level", level},
                    {"System", system },
                    {"Log", message }
                };
                if(AkkaService.Bus != null) //Logging may start before bus is ready
                {
                    var dto = new Dto(data.ToImmutableDictionary());
                    if (_sendEndPoint != null)
                    {
                        await DeCache();
                        await _sendEndPoint.Send(dto);
                    }
                    else
                    {
                        _sendEndPoint = await AkkaService.Bus.GetSendEndpoint(new Uri(_queueUri));
                        await DeCache();
                        await _sendEndPoint.Send(dto);
                    }
                }
                else
                {
                    _logCache.Add(data);
                }
                
            }
            catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
        }
        private void Init(IActorRef sender)
        {            
            sender.Tell(new LoggerInitialized());
        }
        private async Task DeCache()
        {
            if (_logCache.Count > 0)
            {
                foreach (var log in _logCache)
                {
                    await _sendEndPoint.Send(new Dto(log.ToImmutableDictionary()));
                }
                _logCache.Clear();
            }
        }
    }
}
