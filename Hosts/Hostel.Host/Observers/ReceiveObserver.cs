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
            var body = Encoding.UTF8.GetString(context.GetBody());
            _log.Info("PreReceive", $"PreReceive:{body}");
			//Console.WriteLine(body);
            return Task.CompletedTask;
        }

        public Task PostReceive(ReceiveContext context)
        {
            var body = Encoding.UTF8.GetString(context.GetBody());
            var bf = $"PostReceive:{body}, ElapsedTime:{context.ElapsedTime}";
            _log.Info("PostReceive", bf);
            //Console.WriteLine(bf);
            return Task.CompletedTask;
        }

        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
            where T : class
        {
            var body = context.Message;
            var bf = $"PostConsume:{body}, ElapsedTime:{duration.Seconds}, ConsumerType:{consumerType}";
            _log.Info("PostConsume", bf);
            //Console.WriteLine(bf);
            return Task.CompletedTask;
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan elapsed, string consumerType, Exception exception) where T : class
        {
            // called when the message is consumed but the consumer throws an exception
            var body = context.Message;
            var bf = $"ConsumeFault<T>:{body}, ElapsedTime:{elapsed.Seconds}, ConsumerType:{consumerType}, Error:{exception.ToString()}";
            _log.Info("ConsumeFault", bf);
            //Console.WriteLine(bf);
            return Task.CompletedTask;
        }

        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            // called when an exception occurs early in the message processing, such as deserialization, etc.
            var body = Encoding.UTF8.GetString(context.GetBody());
            var bf = $"ReceiveFault:{body}, Error:{exception.ToString()}";
            _log.Info("ReceiveFault", bf);
            //Console.WriteLine(bf);
            return Task.CompletedTask;
        }
    }
}
