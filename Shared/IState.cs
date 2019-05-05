namespace Shared
{
    public interface IState<out TState>
    {
        TState Update(IEvent evnt);
    }
}
