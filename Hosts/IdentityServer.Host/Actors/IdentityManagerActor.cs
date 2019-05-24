using Akka.Actor;
using IdentityServer4.Events;
using Akka.Persistence;
using Shared;
using MassTransit.Command;
using Newtonsoft.Json;
using IdentityServer.Host.Commands;

namespace IdentityServer.Host.Actors
{
    public class IdentityManagerActor: ReceivePersistentActor
    {
        private string _connectionString;
        public IdentityManagerActor(string persistenceId)
        {
            PersistenceId = persistenceId;
            Command<Event>(evt => 
            {
                Persist(evt, e =>
                {
                    Context.System.Log.Log(Akka.Event.LogLevel.DebugLevel, string.Empty, JsonConvert.SerializeObject(evt, Formatting.Indented));
                });
            });
            Command<IMassTransitCommand>(command => 
            {
                PrepareCommand(command);
            });
            Command<CreateAccount>(account => 
            {
                var child = Context.ActorOf(ProcessorActor.Prop());
                child.Tell(account);
            });
            
        }

        public override string PersistenceId { get; }

        public static Props Prop(string persistenceId)
        {
            return Props.Create(() => new IdentityManagerActor(persistenceId));
        }
        
        private void PrepareCommand(IMassTransitCommand command)
        {
            switch (command.Command.ToLower())
            {
                case "createaccount":
                    {
                        var createAccount = new CreateAccount(command.Command, command.Commander, command.CommandId, command.Payload);
                        Self.Tell(createAccount);
                    }break;
            }
        }
    }
}
