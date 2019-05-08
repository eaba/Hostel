
using Hostel.Command;
using Hostel.Model;
using Shared.Repository;
using Shared.Repository.Impl;
using System.Collections.Generic;
using System.Data;
using static Hostel.Model.Construction;

namespace Hostel.Repository
{
    public static class Floor
    {
        public static bool CreateFloor(this IRepository<IDbProperties> repository, FloorSpec spec)
        {
            var floor = spec;
            var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, floor.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@floor", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@floor")
                        };
            var repos = new DbProperties("CreateFloor", dataTypes, string.Empty, true, "@floor");
            var x = repository.Update(new[] { repos });  
            if(x > 0)
            {
                spec.FloorId = repos.Id;
                return true;
            }
            return false;
        }
        public static bool ConstructHostel(this IRepository<IDbProperties> repository, Construction hostel)
        {
            var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@name", SqlDbType.NVarChar, 50, hostel.Detail.Name, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@address", SqlDbType.NVarChar, 50, hostel.Detail.Address, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@hostel", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@hostel")
                        };
            var repos = new DbProperties("ConstructHostel", dataTypes, string.Empty, true, "@hostel");
            var x = repository.Update(new[] { repos });
            if (x > 0)
            {
                hostel.Detail.HostelId = repos.Id;
                return true;
            }
            return false;
        }
    }
}
