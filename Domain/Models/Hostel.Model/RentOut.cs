using System;
using System.Collections.Generic;
using System.Text;

namespace Hostel.Model
{
    public class RentOut
    {
        public Guid Floor { get; }
        public Guid Room { get; }
        public IEnumerable<Tenant.Tenant> Tenants { get; }
        public RentOut(IEnumerable<Tenant.Tenant> tenants, Guid floor, Guid room)
        {
            Tenants = tenants;
            Floor = floor;
            Room = room;
        }
    }
}
