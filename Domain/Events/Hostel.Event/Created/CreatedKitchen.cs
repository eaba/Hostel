using Hostel.Model;
using Shared;

namespace Hostel.Event.Created
{
    public class CreatedKitchen:IEvent
    {
        public readonly KitchenSpec Kitchen;
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public CreatedKitchen(KitchenSpec kitchen)
        {
            Kitchen = kitchen;
        }
    }
}
