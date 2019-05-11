using GreenPipes;
using LogShared;
using MassTransit;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Akka.MassTransit.Logger.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
            var bus = CreateBus();
            bus.Start();
            System.Console.ReadLine();
            bus.Stop();
        }
        private static IBusControl CreateBus()
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost:/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint(host, "logging_akka", ep =>
                {
                    ep.PrefetchCount = 500;
                    ep.UseRetry(r => r.Immediate(5));
                    ep.Consumer<LogConsumer>();
                });
                cfg.ReceiveEndpoint(host, "trace_akka", ep =>
                {
                    ep.PrefetchCount = 500;
                    ep.UseRetry(r => r.Immediate(5));
                    ep.Consumer<TraceConsumer>();
                });

            });
        }
        public class LogConsumer : IConsumer<Dto>
        {
            public Task Consume(ConsumeContext<Dto> context)
            {
                var log = context.Message;
                System.Console.WriteLine(JsonConvert.SerializeObject(log, Formatting.Indented));
                return Task.CompletedTask;
            }
        }
        public class TraceConsumer : IConsumer<Dto>
        {
            public Task Consume(ConsumeContext<Dto> context)
            {
                var log = context.Message;
                System.Console.WriteLine(JsonConvert.SerializeObject(log, Formatting.Indented));
                return Task.CompletedTask;
            }
        }
    }
}
