
namespace Shared
{
    public interface IQuery
    {
        string User { get; } 
        string QueryId { get; } 
        string ReplyToQueue { get; }
    }
}
