using Akka.Persistence;
using System;

namespace Shared.Actors
{
    public class BaseActor<TState> : UntypedPersistentActor
         where TState : class, IState<TState>
    {
        public override string PersistenceId { get; }

        protected TState State { get; private set; }

        public BaseActor(
            ICommandHandler<TState> handler,
            TState defaultState,
            string persistenceId)
        {
            PersistenceId = persistenceId;

            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            State = defaultState ?? throw new ArgumentNullException(nameof(defaultState));
        }

        private readonly ICommandHandler<TState> _handler;

        protected override void OnCommand(object message)
        {
            if (message is ICommand command)
            {
                var handlerResult = _handler.Handle(State, command);

                if (handlerResult.Success)
                {
                    Persist(handlerResult.Event, OnPersist);
                }
                else
                {
                    
                }
            }
        }

        protected override void OnRecover(object persistedEvent)
        {
            if (persistedEvent is IEvent evnt)
            {
                State = State.Update(evnt);
            }

            if (persistedEvent is RecoveryCompleted)
            {
                OnRecoverComplete();
            }
        }

        protected virtual void OnRecoverComplete()
        {

        }

        protected virtual void OnPersist(IEvent persistedEvent)
        {
            State = State.Update(persistedEvent);
        }
    }
}
