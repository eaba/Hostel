using Akka.Persistence;
using Shared.Repository;
using System;

namespace Shared.Actors
{
    public class HostelActor<TState> : ReceivePersistentActor  where TState : class, IState<TState>
    {
        public override string PersistenceId { get; }
        public IRepository<IDbProperties> Repo;
        private int _eventCount = 0;
        private int _snapShotAfter;
        protected TState State { get; private set; }

        public HostelActor(ICommandHandler<TState> handler, TState defaultState,string persistenceId, IRepository<IDbProperties> repository, int snapshotafter = 100)
        {
            _snapShotAfter = snapshotafter;
            PersistenceId = persistenceId;
            Repo = repository;
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            State = defaultState ?? throw new ArgumentNullException(nameof(defaultState));
            Command<ICommand>(command => 
            {
                var handlerResult = _handler.Handle(State, command, Repo);
                if (handlerResult.Success)
                {
                    Persist(handlerResult.Event, OnPersist);
                }
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
        
        protected virtual void OnRecoverComplete()
        {
            
        }
        protected override void PostStop()
        {
            Repo.Close();
            base.PostStop();
        }
        protected virtual void OnSnapshotOffer(TState state)
        {

        }
        protected virtual void OnPersist(IEvent persistedEvent)
        {
            State = State.Update(persistedEvent);
            SaveSnapshotIfNecessary();
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
