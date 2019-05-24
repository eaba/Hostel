
using Hostel.Model;
using Shared;

namespace Hostel.Command
{
    public sealed class RentOutRoom : Message, ICommand
    {
        public readonly RentOut RentOut;
        public string Commander { get; }
        public string CommandId { get; }
        public RentOutRoom(RentOut rentOut, string commander, string commandid)
        {
            RentOut = rentOut;
            Commander = commander;
            CommandId = commandid;
        }        
    }
}
