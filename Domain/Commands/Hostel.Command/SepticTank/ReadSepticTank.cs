using Shared;

namespace Hostel.Command.SepticTank
{
    public sealed class ReadSepticTank : Message, ICommand
    {
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
    }
}
