
namespace Hostel.Model
{
    public class Sensor
    {
        public string SensorId { get; }
        public string Role { get; }
        public string Tag { get; }
        public Sensor(string id, string role, string tag)
        {
            SensorId = id;
            Tag = tag;
            Role = role;
        }
    }
}
