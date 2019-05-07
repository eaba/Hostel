using Shared;

namespace Hostel.Event
{
    public class CreatedFloor:IEvent
    {
        public readonly Model.Floor Floor;
        public string Commander { get; }
        public string CommandId { get; }
        public CreatedFloor(Model.Floor floor, string commander, string commandid)
        {
            Floor = floor;
            Commander = commander;
            CommandId = commandid;
        }        
    }
}
