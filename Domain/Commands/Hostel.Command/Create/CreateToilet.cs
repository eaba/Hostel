using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public sealed class CreateToilet : Message, ICommand
    {
        public readonly ToiletSpec Toilet;
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
        public CreateToilet(ToiletSpec toilet)
        {
           Toilet = toilet;
        }
    }
}
