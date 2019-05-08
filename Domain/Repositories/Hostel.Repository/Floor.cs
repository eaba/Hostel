
using Hostel.Command;
using Hostel.Model;
using Shared.Repository;
using Shared.Repository.Impl;
using System.Collections.Generic;
using System.Data;

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
    }
}
