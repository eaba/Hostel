using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public sealed class CreateToilet : Message, ICommand
    {
        public readonly ToiletSpec Toilet;
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public CreateToilet(ToiletSpec toilet)
        {
           Toilet = toilet;
        }
    }
}
