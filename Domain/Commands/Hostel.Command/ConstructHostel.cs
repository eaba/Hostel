using Hostel.Model;
using Shared;

namespace Hostel.Command
{
    public class ConstructHostel : Message, ISystemCommand
    {
        public Construction Construction { get; }
        public ConstructHostel(Construction construction)
        {
            Construction = construction;
        }
    }
}
