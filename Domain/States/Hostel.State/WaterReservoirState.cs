using Hostel.State.Sensor;
using Shared;

namespace Hostel.State
{
    public class WaterReservoirState : Message, IState<WaterReservoirState>
    {
        public int Height { get; }
        public Reading Previous { get; }
        public Reading Current { get; }
        public int AlertHeight { get; }
        public static readonly WaterReservoirState Empty = new WaterReservoirState();
        public WaterReservoirState Update(IEvent evnt)
        {
            throw new System.NotImplementedException();
        }
    }
}
