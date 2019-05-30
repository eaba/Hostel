using Shared;
namespace Hostel.Command
{
    public sealed class InstallSensor : Message, ICommand
    {
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
    }
}
