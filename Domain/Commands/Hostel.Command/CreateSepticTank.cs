using Hostel.Model;
using Shared;
using System.Collections.Generic;

namespace Hostel.Command
{
    public class CreateSepticTank : Message, ICommand
    {
        public string Commander => string.Empty;

        public string CommandId => string.Empty;
        public int Height { get; }
        public string Tag { get; }
        public IEnumerable<SensorSpec> Sensors { get; }
        public CreateSepticTank(string tag, int height, IEnumerable<SensorSpec> sensors)
        {
            Tag = tag;
            Height = height;
            Sensors = sensors;
        }
    }
}
