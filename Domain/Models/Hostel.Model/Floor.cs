using System;

namespace Hostel.Model
{
    public class Floor
    {
        public Guid FloorId { get; }
        public string Tag { get; }
        public Floor(Guid floor, string tag)
        {
            FloorId = floor;
            Tag = tag;
        }
    }
}
