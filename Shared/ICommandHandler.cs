using Shared.Repository;

namespace Shared
{
    public interface ICommandHandler<in TState>
    {
        HandlerResult Handle(TState state, ICommand command, IRepository<IDbProperties> repository);
    }
}
