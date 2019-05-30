using Hostel.Model;
using Shared;

namespace Hostel.Command.Create
{
    public sealed class CreateWaterReservoir : Message, ICommand
    {
        public string Commander { get; }
        public string CommandId { get; }
        public string ReplyToQueue { get; }
        public ReservoirSpec Spec { get; }
        public CreateWaterReservoir(ReservoirSpec spec)
        {
            Spec = spec;
        }
    }
}
