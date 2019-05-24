using Shared;

namespace Hostel.Command
{
    public sealed class EvictTenant : Message, ICommand
    {
        public string Commander => throw new System.NotImplementedException();

        public string CommandId => throw new System.NotImplementedException();
    }
}
