using Hostel.Model;
using Shared;

namespace Hostel.Command
{
    public sealed class ConstructHostel : Message, ICommand
    {
        public Construction Construction { get; }

        public string Commander => string.Empty;

        public string CommandId => string.Empty;

        public ConstructHostel(Construction construction)
        {
            Construction = construction;
        }
    }
}
