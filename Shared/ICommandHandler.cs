namespace Shared
{
    public interface ICommandHandler<in TState>
    {
        HandlerResult Handle(TState state, ICommand command);
    }
}
