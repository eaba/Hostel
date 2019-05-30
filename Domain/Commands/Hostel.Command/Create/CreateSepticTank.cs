using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public sealed class CreateSepticTank : Message, ICommand
    {
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
        public SepticTankSpec Spec { get; }
        public CreateSepticTank(SepticTankSpec spec)
        {
            Spec = spec;
        }
    }
}
