using Hostel.Model;
using Shared;

namespace Hostel.Event.Created
{
    public class CreatedToilet:IEvent
    {
        public readonly ToiletSpec Toilet;
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
        public CreatedToilet(ToiletSpec toilet)
        {
            Toilet = toilet;
        }
    }
}
