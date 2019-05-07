using Shared;
using System;

namespace Hostel.State
{
    public class WaterReservoirState : Message, IState<WaterReservoirState>
    {
        public int Dept { get; }
        public int CurrentReading { get; }
        public int AlertWhenReadingIs { get; }
        public string WhoToAlert { get; }
        public static readonly WaterReservoirState Empty = new WaterReservoirState(0);
        public WaterReservoirState(int current):this(current, 100, 75, string.Empty)
        {

        }
        public WaterReservoirState(int current, int dept, int alert, string who)
        {

        }
        public WaterReservoirState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
