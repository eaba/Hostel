using Akka.Actor;
using Akka.Event;
using MassTransit;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Hostel.Host.Observers
{
    class ReceiveObserver : IReceiveObserver
    {
        private readonly ILoggingAdapter _log;
        public ReceiveObserver(ActorSystem actorSystem)
        {
            _log = Logging.GetLogger(actorSystem, actorSystem, null);
        }
        public Task PreReceive(ReceiveContext context)
        {
            // called immediately after the message was delivery by the transport
            _log.Info("",JsonConvert.SerializeObject(context, Formatting.Indented));
            return Task.CompletedTask;
        }

        public Task PostReceive(ReceiveContext context)
        {
            // called after the message has been received and processed
            _log.Info("",JsonConvert.SerializeObject(context, Formatting.Indented));
            return Task.CompletedTask;
        }

        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
            where T : class
        {
            // called when the message was consumed, once for each consumer
            _log.Info("",JsonConvert.SerializeObject(context, Formatting.Indented));
            return Task.CompletedTask;
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan elapsed, string consumerType, Exception exception) where T : class
        {
            // called when the message is consumed but the consumer throws an exception
            _log.Info("", JsonConvert.SerializeObject(context, Formatting.Indented));
            return Task.CompletedTask;
        }

        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            // called when an exception occurs early in the message processing, such as deserialization, etc.
            _log.Info("", exception.ToString());
            return Task.CompletedTask;
        }
    }
}
