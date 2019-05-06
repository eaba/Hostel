using Akka.Persistence;
using System;

namespace Shared.Actors
{
    public class HostelActor<TState> : ReceivePersistentActor  where TState : class, IState<TState>
    {
        public override string PersistenceId { get; }
        private int _eventCount = 0;
        private int _snapShotAfter = 100;
        protected TState State { get; private set; }

        public HostelActor(ICommandHandler<TState> handler, TState defaultState,string persistenceId)
        {
            PersistenceId = persistenceId;
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            State = defaultState ?? throw new ArgumentNullException(nameof(defaultState));
            Command<ICommand>(command => 
            {
                var handlerResult = _handler.Handle(State, command);
                if (handlerResult.Success)
                {
                    Persist(handlerResult.Event, OnPersist);
                }
                else
                {

                }
            });
            Recover<IEvent>(evnt => { State = State.Update(evnt); });
            Recover<RecoveryCompleted>(completed => { OnRecoverComplete(); });
        }

        private readonly ICommandHandler<TState> _handler;
        
        protected virtual void OnRecoverComplete()
        {

        }

        protected virtual void OnPersist(IEvent persistedEvent)
        {
            State = State.Update(persistedEvent);
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
