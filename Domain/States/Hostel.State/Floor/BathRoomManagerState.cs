using Hostel.Model;
using Shared;
using System;
using System.Collections.Generic;

namespace Hostel.State.Floor
{
    public class BathRoomManagerState : Message, IState<BathRoomManagerState>
    {
        public string Tag { get; }
        public IEnumerable<SensorSpec> Sensors { get; }
        public BathRoomManagerState(IEnumerable<SensorSpec> sensors, string tag)
        {
            Tag = tag;
            Sensors = sensors;
        }

        public BathRoomManagerState Update(IEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}
