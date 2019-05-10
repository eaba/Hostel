using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public class CreateKitchen : Message, ICommand
    {
        public readonly KitchenSpec Kitchen;
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public CreateKitchen(KitchenSpec kitchen)
        {
            Kitchen = kitchen;
        }
    }
}
