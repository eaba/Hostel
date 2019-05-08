using Hostel.Model;
using Hostel.State.Sensor;
using Shared;
using System;
using System.Collections.Generic;

namespace Hostel.State
{
    public class SepticTankState : Message, IState<SepticTankState>
    {
        public int Height { get; }
        public Reading Previous { get; }
        public Reading Current { get; }
        public int AlertHeight { get; }
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
