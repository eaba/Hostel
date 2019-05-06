using System;
using System.Collections.Generic;
using System.Text;

namespace Sensor.Model
{
    public class Sensor
    {
        public readonly Dictionary<string, string> Device;
        public Sensor(Dictionary<string, string> device)
        {
            Device = device;
        }
    }
}
