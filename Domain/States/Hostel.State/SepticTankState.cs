using Shared;
using System;

namespace Hostel.State
{
    public class SepticTankState : Message, IState<SepticTankState>
    {
        public int Dept { get; }
        public int CurrentReading { get; }
        public int AlertWhenReadingIs { get; }
        public string WhoToAlert { get; }
        public static readonly SepticTankState Empty = new SepticTankState(0);
        public SepticTankState(int current):this(current, 100, 75, string.Empty)
        {

        }
        public SepticTankState(int current, int dept, int alert, string who)
        {

        }
        public SepticTankState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
