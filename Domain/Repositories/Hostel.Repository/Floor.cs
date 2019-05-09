using Hostel.Model;
using Hostel.State;
using Shared.Repository;
using Shared.Repository.Impl;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Hostel.Repository
{
    public static class Floor
    {
        public static bool CreateFloor(this IRepository<IDbProperties> repository, FloorSpec spec)
        {
            var floor = spec;
            var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@hostel", SqlDbType.UniqueIdentifier, 0, spec.HostelId, ParameterDirection.Input, false, false, ""),
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
        public static bool InstallSepticTankSensors(this IRepository<IDbProperties> repository, SepticTankState state, out SepticTankState septicTankState)
        {
            var sensors = state.Sensors;
            var dbProperties = new List<IDbProperties>();
            foreach(var sensor in sensors)
            {
                var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@septictank", SqlDbType.UniqueIdentifier, 0, state.SepticTankId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, sensor.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@role", SqlDbType.NVarChar, 50, sensor.Role, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@sensorid", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@sensorid")
                        };
                var repos = new DbProperties("InstallSepticSensor", dataTypes, string.Empty, true, "@sensorid", sensor.Tag);
            }            
            var x = repository.Update(dbProperties);
            if (x > 0)
            {
                foreach(var sensor in sensors)
                {
                    var outputs = repository.OutPuts;
                    sensor.SensorId = outputs.ToList().FirstOrDefault(y => y.Identifier == sensor.Tag).Value;
                }
                septicTankState = new SepticTankState(state.SepticTankId, state.Height, state.AlertHeight, sensors);
                return true;
            }
            septicTankState = state;
            return false;
        }
        public static bool InstallReservoirSensors(this IRepository<IDbProperties> repository, WaterReservoirState state, out WaterReservoirState waterReservoirState)
        {
            var sensors = state.Sensors;
            var dbProperties = new List<IDbProperties>();
            foreach (var sensor in sensors)
            {
                var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@reservoir", SqlDbType.UniqueIdentifier, 0, state.ReservoirId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, sensor.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@role", SqlDbType.NVarChar, 50, sensor.Role, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@sensorid", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@sensorid")
                        };
                var repos = new DbProperties("InstallReservoirSensor", dataTypes, string.Empty, true, "@sensorid", sensor.Tag);
            }
            var x = repository.Update(dbProperties);
            if (x > 0)
            {
                foreach (var sensor in sensors)
                {
                    var outputs = repository.OutPuts;
                    sensor.SensorId = outputs.ToList().FirstOrDefault(y => y.Identifier == sensor.Tag).Value;
                }
                waterReservoirState = new WaterReservoirState(state.ReservoirId, state.Height, state.AlertHeight, sensors);
                return true;
            }
            waterReservoirState = state;
            return false;
        }
        public static bool CreateSepticTank(this IRepository<IDbProperties> repository, SepticTankSpec spec)
        {
            var septic = spec;
            var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@hostel", SqlDbType.UniqueIdentifier, 0, spec.HostelId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, septic.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@height", SqlDbType.Int, 0, septic.Height.ToString(), ParameterDirection.Input, false, false, ""),
                            new DataTypes("@septictank", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@septictank")
                        };
            var repos = new DbProperties("CreateSepticTank", dataTypes, string.Empty, true, "@septictank");
            var x = repository.Update(new[] { repos });
            if (x > 0)
            {
                spec.SepticTankId = repos.Id;
                return true;
            }
            return false;
        }
        public static bool CreateReservoir(this IRepository<IDbProperties> repository, ReservoirSpec spec)
        {
            var water = spec;
            var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@hostel", SqlDbType.UniqueIdentifier, 0, spec.HostelId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, water.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@height", SqlDbType.Int, 0, water.Height.ToString(), ParameterDirection.Input, false, false, ""),
                            new DataTypes("@reservoir", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@reservoir")
                        };
            var repos = new DbProperties("CreateReservoir", dataTypes, string.Empty, true, "@reservoir");
            var x = repository.Update(new[] { repos });
            if (x > 0)
            {
                spec.ReservoirId = repos.Id;
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
