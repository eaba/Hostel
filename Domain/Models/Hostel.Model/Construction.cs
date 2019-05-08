
using System.Collections.Generic;

namespace Hostel.Model
{
    public class Construction
    {
        public List<FloorSpec> Floors;
        public SepticTankSpec SepticTank;
        public ReservoirSpec Reservoir;
        public Construction()
        {
            Floors = new List<FloorSpec>();
        }
        public Construction WithFloor(string floorTag, string floor, int room, string roomprefix, int toilet, string toiletprefix, int baths, string bathprefix, string kitTag)
        {
            var rooms = new List<RoomSpecs>();
            var toilets = new List<ToiletSpec>();
            var bathrooms = new List<BathRoomSpec>();

            for (var i = 0; i <= room; ++i)
            {
                rooms.Add(new RoomSpecs(roomprefix, i.ToString("00")));
            }
            for (var i = 0; i <= toilet; ++i)
            {
                var tolet = new ToiletSpec
                {
                    Tag = toiletprefix + i.ToString("00")
                };
                tolet.Sensors = new List<SensorSpec>(new[] 
                {
                    new SensorSpec($"Sensor_{tolet.Tag}", "Dirth"),
                    new SensorSpec($"Sensor_{tolet.Tag}", "Smell"),
                    new SensorSpec($"Sensor_{tolet.Tag}", "Flush")
                });
                toilets.Add(tolet);
            }
            for (var i = 0; i <= baths; ++i)
            {
                var bath = new BathRoomSpec
                {
                    Tag = bathprefix + i.ToString("00")
                };
                bath.Sensors = new List<SensorSpec>(new[]
                {
                    new SensorSpec($"Sensor_{bath.Tag}_A", "Dirth"),
                    new SensorSpec($"Sensor_{bath.Tag}_B", "Dirth"),
                    new SensorSpec($"Sensor_{bath.Tag}", "Smell")
                });
                bathrooms.Add(bath);
            }
            var kitchen = new KitchenSpec
            {
                Tag = kitTag,
                Sensors = new List<SensorSpec>(new[]
                {
                    new SensorSpec($"Sensor_{kitTag}_A", "Dirth"),
                    new SensorSpec($"Sensor_{kitTag}_B", "Dirth"),
                    new SensorSpec($"Sensor_{kitTag}", "Smell")
                })
            };
            Floors.Add(new FloorSpec(floorTag, rooms, toilets, bathrooms, kitchen));
            return this;
        }
        public Construction WithSepticTank(string tag, int height)
        {
            var septic = new SepticTankSpec(tag, height)
            {
                Sensors = new List<SensorSpec>(new[]
                {
                    new SensorSpec($"Sensor_{tag}_A", "Sonic"),
                    new SensorSpec($"Sensor_{tag}_B", "Sonic"),
                    new SensorSpec($"Sensor_{tag}_C", "Sonic")
                })
            };
            SepticTank = septic;
            return this;
        }
        public Construction WithReservoir(string tag, int height)
        {
            var reservoir = new ReservoirSpec(tag, height)
            {
                Sensors = new List<SensorSpec>(new[]
                {
                    new SensorSpec($"Sensor_{tag}_A", "Sonic"),
                    new SensorSpec($"Sensor_{tag}_B", "Sonic"),
                    new SensorSpec($"Sensor_{tag}_C", "Sonic")
                })
            };
            Reservoir = reservoir;
            return this;
        }
    }
    public class FloorSpec
    {
        public string FloorId;
        public string Tag { get; }
        public IEnumerable<RoomSpecs> Rooms { get; }
        public IEnumerable<ToiletSpec> Toilets { get; }
        public IEnumerable<BathRoomSpec> BathRooms { get; }
        public KitchenSpec Kitchen { get; }
        public FloorSpec(string tag, IEnumerable<RoomSpecs> rooms, IEnumerable<ToiletSpec> toilet, IEnumerable<BathRoomSpec> baths, KitchenSpec kitchen)
        {
            Tag = tag;
            Rooms = rooms;
            Toilets = toilet;
            BathRooms = baths;
            Kitchen = kitchen;
        }

    }
    public class RoomSpecs
    {
        public string Tag { get; }
        public RoomSpecs(string roomTag, string tag)
        {
            Tag = roomTag+tag;
        }
    }
    public class ToiletSpec
    {
        public string Tag;
        public IEnumerable<SensorSpec> Sensors;
    }
    public class BathRoomSpec
    {
        public string Tag;
        public IEnumerable<SensorSpec> Sensors;
    }
    public class KitchenSpec
    {
        public string Tag;
        public IEnumerable<SensorSpec> Sensors;
    }
    public class SepticTankSpec
    {
        public string Tag { get; }
        public int Height { get; }
        public IEnumerable<SensorSpec> Sensors;
        public SepticTankSpec(string tag, int height)
        {
            Tag = tag;
            Height = height;
        }
    }
    public class ReservoirSpec
    {
        public string Tag { get; }
        public int Height { get; }
        public IEnumerable<SensorSpec> Sensors;
        public ReservoirSpec(string tag, int height)
        {
            Tag = tag;
            Height = height;
        }
    }
    public class SensorSpec
    {
        public string Tag { get; }
        public string Role { get; }
        public SensorSpec(string tag, string role)
        {
            Tag = tag+"_"+role;
            Role = role;
        }
    }
}
