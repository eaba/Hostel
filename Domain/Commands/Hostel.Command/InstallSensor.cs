using Shared;
namespace Hostel.Command
{
    public sealed class InstallSensor : Message, ICommand
    {
        public string Commander => string.Empty;
        public string CommandId => string.Empty;
    }
}
