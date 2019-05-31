using Akka.Actor;
using Akka.Persistence;
using Newtonsoft.Json;
using Shared.Repository;
using System;
using System.Data.SqlClient;

namespace Shared.Actors
{
    public class HostelActor<TState> : ReceivePersistentActor  where TState : class, IState<TState>
    {
        public override string PersistenceId { get; }
        private int _eventCount = 0;
        private int _snapShotAfter;
        protected TState State { get; private set; }
        private SqlConnection _connection;
        public IRepository<IDbProperties> Repository;

        public HostelActor(ICommandHandler<TState> handler, TState defaultState, string persistenceId, string connectionString, int snapshotafter = 100)
        {

            _snapShotAfter = snapshotafter;
            PersistenceId = persistenceId;
            _connection = new SqlConnection(connectionString);
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            State = defaultState ?? throw new ArgumentNullException(nameof(defaultState));
            Command<ICommand>(command =>
            {
                SaveCommand(command);
                var handlerResult = _handler.Handle(State, command, Repository);
                if (handlerResult.Success)
                {
                    Persist(handlerResult.Event, @event => OnPersist(@event, command.CommandId));
                }
                if(!string.IsNullOrWhiteSpace(command.ReplyToQueue) && !string.IsNullOrWhiteSpace(command.CommandId) && !string.IsNullOrWhiteSpace(command.Commander))
                {
                    NotifyUI(command, handlerResult);
                }
                var state = JsonConvert.SerializeObject(State, Formatting.Indented);
                Context.System.Log.Info($"{State.GetType().Name}:{state}");
            });
            Recover<IEvent>(evnt => { State = State.Update(evnt); });
            Recover<SnapshotOffer>(offer =>
            {
                if (offer.Snapshot is TState)
                {
                    State = (TState)offer.Snapshot;
                    OnSnapshotOffer(State);
                }
            });
            Recover<RecoveryCompleted>(completed => { OnRecoverComplete(); });
        }

        private readonly ICommandHandler<TState> _handler;
        protected override void PreStart()
        {
            _connection.Open();
            Repository = new Repository.Impl.Repository(_connection);
            base.PreStart();
        }
        protected virtual void OnRecoverComplete()
        {
            
        }
        protected override void PostStop()
        {
            Repository.Close();
            base.PostStop();
        }
        private void DeleteCommand(IEvent @event, string commandid)
        {
            if(!string.IsNullOrWhiteSpace(commandid))
            {
                if (State.PendingCommands.ContainsKey(commandid))
                {
                    State.PendingCommands.Remove(commandid);
                }
            }
        }
        private void SaveCommand(ICommand command)
        {
            if(!string.IsNullOrWhiteSpace(command.CommandId))
            {
                if (!State.PendingCommands.ContainsKey(command.CommandId))
                {
                    State.PendingCommands.Add(command.CommandId, command);
                }
            }
        }
        protected virtual void OnSnapshotOffer(TState state)
        {

        }
        protected virtual void NotifyUI(ICommand command, HandlerResult result)
        {

        }
        protected virtual void OnPersist(IEvent persistedEvent, string commandid)
        {
            DeleteCommand(persistedEvent, commandid);
            State = State.Update(persistedEvent);
            SaveSnapshotIfNecessary();
        }
        
        protected override void OnReplaySuccess()
        {
            base.OnReplaySuccess();
            foreach (var command in State.PendingCommands)
            {
                Self.Tell(command.Value);
            }
        }
        private void SaveSnapshotIfNecessary()
        {
            _eventCount = (_eventCount + 1) % _snapShotAfter;
            if (_eventCount == 0)
            {
                SaveSnapshot(State);
            }
        }
    }
}
