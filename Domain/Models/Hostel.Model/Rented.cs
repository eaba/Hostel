using System.Collections.Generic;

namespace Hostel.Model
{
    public class Rented
    {
        public readonly Dictionary<string, string> Room;
        public Rented(Dictionary<string, string> room)
        {
            Room = room;
        }
    }
}
