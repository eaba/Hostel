
using System.Collections.Generic;

namespace Hostel.Model
{
    public class Construction
    {
        public HostelDetail Detail { get; }
        public List<FloorSpec> Floors;
        public SepticTankSpec SepticTank;
        public ReservoirSpec Reservoir;
        public Construction(HostelDetail detail)
        {
            Detail = detail;
            Floors = new List<FloorSpec>();
        }
        public class HostelDetail
        {
            public string HostelId;
            public string Name { get; }
            public string Address { get; }
            public HostelDetail(string name, string address)
            {
                Name = name;
                Address = address;
            }
        }
        public Construction WithFloor(string floorTag, string floor, int room, string roomprefix, int toilet, string toiletprefix, int baths, string bathprefix, string kitTag)
        {
            var rooms = new List<RoomSpecs>();
            var toilets = new List<ToiletSpec>();
            var bathrooms = new List<BathRoomSpec>();

            for (var i = 0; i <= room; ++i)
            {
                rooms.Add(new RoomSpecs { Tag = roomprefix + i.ToString("00") });
            }
            for (var i = 0; i <= toilet; ++i)
            {
                var tolet = new ToiletSpec
                {
                    Tag = toiletprefix + floor + i.ToString("00")
                };
                tolet.Sensors = new List<SensorSpec>(new[] 
                {
                    new SensorSpec($"{tolet.Tag}", "Dirt"),
                    new SensorSpec($"{tolet.Tag}", "Smell"),
                    new SensorSpec($"{tolet.Tag}", "Flush")
                });
                toilets.Add(tolet);
            }
            for (var i = 0; i <= baths; ++i)
            {
                var bath = new BathRoomSpec
                {
                    Tag = bathprefix + floor + i.ToString("00")
                };
                bath.Sensors = new List<SensorSpec>(new[]
                {
                    new SensorSpec($"{bath.Tag}_A", "Dirt"),
                    new SensorSpec($"{bath.Tag}_B", "Dirt"),
                    new SensorSpec($"{bath.Tag}", "Smell")
                });
                bathrooms.Add(bath);
            }
            var kitchen = new KitchenSpec
            {
                Tag = kitTag + floor
            };
            kitchen.Sensors = new List<SensorSpec>(new[]
                {
                    new SensorSpec($"{kitchen.Tag}_A", "Dirt"),
                    new SensorSpec($"{kitchen.Tag}_B", "Dirt"),
                    new SensorSpec($"{kitchen.Tag}", "Smell")
                });
            Floors.Add(new FloorSpec(floorTag, rooms, toilets, bathrooms, kitchen));
            return this;
        }
        public Construction WithSepticTank(string tag, int height, int alert)
        {
            var septic = new SepticTankSpec(tag, height, alert)
            {
                Sensors = new List<SensorSpec>(new[]
                {
                    new SensorSpec($"{tag}_A", "Sonic"),
                    new SensorSpec($"{tag}_B", "Sonic"),
                    new SensorSpec($"{tag}_C", "Sonic")
                })
            };
            SepticTank = septic;
            return this;
        }
        public Construction WithReservoir(string tag, int height, int alert)
        {
            var reservoir = new ReservoirSpec(tag, height, alert)
            {
                Sensors = new List<SensorSpec>(new[]
                {
                    new SensorSpec($"{tag}_A", "Sonic"),
                    new SensorSpec($"{tag}_B", "Sonic"),
                    new SensorSpec($"{tag}_C", "Sonic")
                })
            };
            Reservoir = reservoir;
            return this;
        }
    }
    public class FloorSpec
    {
        public string HostelId;
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
        public string RoomId;
        public string FloorId;
        public string Tag;
        public IEnumerable<SensorSpec> Sensors;
    }
    public class ToiletSpec
    {
        public string ToiletId;
        public string FloorId;
        public string Tag;
        public IEnumerable<SensorSpec> Sensors;
    }
    public class BathRoomSpec
    {
        public string BathRoomId;
        public string FloorId;
        public string Tag;
        public IEnumerable<SensorSpec> Sensors;
    }
    public class KitchenSpec
    {
        public string KitchenId;
        public string FloorId;
        public string Tag;
        public IEnumerable<SensorSpec> Sensors;
    }
    public class SepticTankSpec
    {
        public string HostelId;
        public string SepticTankId;
        public string Tag { get; }
        public int Height { get; }// Feet
        public int AlertHeight { get; } //Feet
        public IEnumerable<SensorSpec> Sensors;
        public SepticTankSpec(string tag, int height, int alert)
        {
            Tag = tag;
            Height = height;
            AlertHeight = alert;
        }
    }
    public class ReservoirSpec
    {
        public string HostelId;
        public string ReservoirId;
        public string Tag { get; }
        public int Height { get; }//Feet
        public int AlertHeight { get; }//Feet
        public IEnumerable<SensorSpec> Sensors;
        public ReservoirSpec(string tag, int height, int alert)
        {
            Tag = tag;
            Height = height;
            AlertHeight = alert;
        }
    }
    public class SensorSpec
    {
        public string SensorId;
        public string Tag { get; }
        public string Role { get; }
        public SensorSpec(string tag, string role)
        {
            Tag = tag+"_"+role;
            Role = role;
        }
    }
}
