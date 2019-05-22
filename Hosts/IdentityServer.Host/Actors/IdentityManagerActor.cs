using Akka.Actor;
using IdentityServer4.Events;
using Akka.Persistence;
using Shared;
using MassTransit.Command;

namespace IdentityServer.Host.Actors
{
    public class IdentityManagerActor: ReceivePersistentActor
    {
        private string _connectionString;
        public IdentityManagerActor(string persistenceId)
        {
            PersistenceId = persistenceId;
            Command<Event>(evt => {
                Persist(evt, e =>{

                });
            });
            Command<IMassTransitCommand>(command => {
                PrepareCommand(command);
            });
            Command<CreateAccount>(account => { });
            
        }

        public override string PersistenceId { get; }

        public static Props Prop(string persistenceId)
        {
            return Props.Create(() => new IdentityManagerActor(persistenceId));
        }
        
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(maxNrOfRetries: 100, withinTimeMilliseconds: 1000, loggingEnabled: true,
                decider: Decider.From(x =>
                {
                    return Directive.Restart;
                }));
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
