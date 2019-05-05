using System.Collections.Generic;

namespace Hostel.Model
{
    public struct Rented
    {
        public readonly Dictionary<string, string> Room;
        public Rented(Dictionary<string, string> room)
        {
            Room = room;
        }
    }
}
