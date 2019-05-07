
namespace Hostel.Model.SepticTank
{
    public class SepticTankReading
    {
        string Value { get; }
        string TimeStamp { get; }
        string SensorId { get; }
        public SepticTankReading(string value, string timestamp, string sensorid)
        {
            Value = value;
            TimeStamp = timestamp;
            SensorId = sensorid;
        }
    }
}
