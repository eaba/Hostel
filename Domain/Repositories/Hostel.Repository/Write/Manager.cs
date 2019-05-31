using Shared.Repository;
using Shared.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hostel.Repository.Write
{
    public static class  Manager
    {
        public static bool CreatePerson(this IRepository<IDbProperties> repository, Dictionary<string, string> inputs)
        {
            var dbps = new List<DbProperties>();
            var birthday = DateTime.Parse(inputs["birthday"]);
            var date = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@firstName", SqlDbType.NVarChar, 50, inputs["firstName"], ParameterDirection.Input, false, false, ""),
                            new DataTypes("@lastName", SqlDbType.NVarChar, 50, inputs["lastName"], ParameterDirection.Input, false, false, ""),
                            new DataTypes("@phone", SqlDbType.NVarChar, 50, inputs["phone"], ParameterDirection.Input, false, false, ""),
                            new DataTypes("@email", SqlDbType.NVarChar, 50, inputs["email"], ParameterDirection.Input, false, false, ""),
                            new DataTypes("@date", SqlDbType.NVarChar, 50, date, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@role", SqlDbType.NVarChar, 50, inputs["role"], ParameterDirection.Input, false, false, ""),
                            new DataTypes("@person", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@person")
                        };
            dbps.Add(new DbProperties("CreatePerson", dataTypes, string.Empty, true, "@person", "CreatePerson"));
            dataTypes = new List<IDataTypes>
            {
                 new DataTypes("@person", SqlDbType.UniqueIdentifier, 0, "", ParameterDirection.Input, false, true, "@person"),
                 new DataTypes("@day", SqlDbType.Int, 0, birthday.Day.ToString(), ParameterDirection.Input, false, false, ""),
                 new DataTypes("@month", SqlDbType.Int, 0, birthday.Month.ToString(), ParameterDirection.Input, false, false, ""),
                 new DataTypes("@year", SqlDbType.Int, 0, birthday.Year.ToString(), ParameterDirection.Input, false, false, ""),
            };
            dbps.Add(new DbProperties("CreatePerson_Birthday", dataTypes, "", false, ""));
            var x = repository.Update(dbps);
            var personId = repository.OutPuts.Where(i => i.Identifier == "CreatePerson").FirstOrDefault().Value;
            if (x > 0 || !string.IsNullOrWhiteSpace(personId))
            {
                inputs["person"] = personId;
                return true;
            }
            return false;
        }
    }
}
