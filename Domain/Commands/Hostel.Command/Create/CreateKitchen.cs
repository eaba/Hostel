using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public sealed class CreateKitchen : Message, ICommand
    {
        public readonly KitchenSpec Kitchen;
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
        public CreateKitchen(KitchenSpec kitchen)
        {
            Kitchen = kitchen;
        }
    }
}
