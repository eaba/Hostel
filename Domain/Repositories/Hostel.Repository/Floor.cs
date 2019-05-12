using Hostel.Model;
using Hostel.State;
using Hostel.State.Floor.Units;
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
                            new DataTypes("@hostel", SqlDbType.NVarChar, 50, spec.HostelId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, floor.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@floor", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@floor")
                        };
            var repos = new DbProperties("CreateFloor", dataTypes, string.Empty, true, "@floor");
            var x = repository.Update(new[] { repos });  
            if(x > 0 || !string.IsNullOrWhiteSpace(repos.Id))
            {
                spec.FloorId = repos.Id;
                return true;
            }
            return false;
        }
        public static bool CreateKitchen(this IRepository<IDbProperties> repository, KitchenSpec spec)
        {
            var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@floor", SqlDbType.NVarChar, 50, spec.FloorId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, spec.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@kitchen", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@kitchen")
                        };
            var repos = new DbProperties("CreateKitchen", dataTypes, string.Empty, true, "@kitchen");
            var x = repository.Update(new[] { repos });
            if (x > 0 || !string.IsNullOrWhiteSpace(repos.Id))
            {
                spec.KitchenId = repos.Id;
                return true;
            }
            return false;
        }
        public static bool CreateRoom(this IRepository<IDbProperties> repository, RoomSpecs spec)
        {
            var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@floor", SqlDbType.NVarChar, 50, spec.FloorId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, spec.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@room", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@room")
                        };
            var repos = new DbProperties("CreateRoom", dataTypes, string.Empty, true, "@room");
            var x = repository.Update(new[] { repos });
            if (x > 0 || !string.IsNullOrWhiteSpace(repos.Id))
            {
                spec.RoomId = repos.Id;
                return true;
            }
            return false;
        }
        public static bool CreateBathRoom(this IRepository<IDbProperties> repository, BathRoomSpec spec)
        {
            var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@floor", SqlDbType.NVarChar, 50, spec.FloorId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, spec.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@bathroom", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@bathroom")
                        };
            var repos = new DbProperties("CreateBathRoom", dataTypes, string.Empty, true, "@bathroom");
            var x = repository.Update(new[] { repos });
            if (x > 0 || !string.IsNullOrWhiteSpace(repos.Id))
            {
                spec.BathRoomId = repos.Id;
                return true;
            }
            return false;
        }
        public static bool CreateToilet(this IRepository<IDbProperties> repository, ToiletSpec spec)
        {
            var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@floor", SqlDbType.NVarChar, 50, spec.FloorId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, spec.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@toilet", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@toilet")
                        };
            var repos = new DbProperties("CreateToilet", dataTypes, string.Empty, true, "@toilet");
            var x = repository.Update(new[] { repos });
            if (x > 0 || !string.IsNullOrWhiteSpace(repos.Id))
            {
                spec.ToiletId = repos.Id;
                return true;
            }
            return false;
        }
        public static bool InstallKitchenSensors(this IRepository<IDbProperties> repository, KitchenState state, out KitchenState kitchenNewState)
        {
            var sensors = state.Sensors;
            var dbProperties = new List<IDbProperties>();
            foreach (var sensor in sensors.ToList())
            {
                var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@kitchen", SqlDbType.NVarChar, 50, state.KitchenId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, sensor.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@role", SqlDbType.NVarChar, 50, sensor.Role, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@sensorid", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@sensorid")
                        };
                var repos = new DbProperties("InstallKitchenSensor", dataTypes, string.Empty, true, "@sensorid", sensor.Tag);
                dbProperties.Add(repos);
            }
            var x = repository.Update(dbProperties);
            if (x > 0)
            {
                var sensorsSpec = new List<SensorSpec>();
                foreach (var sensor in sensors.ToList())
                {
                    var outputs = repository.OutPuts;
                    sensor.SensorId = outputs.ToList().FirstOrDefault(y => y.Identifier == sensor.Tag).Value;
                    sensorsSpec.Add(sensor);
                }
                kitchenNewState = new KitchenState(state.KitchenId, state.Tag, sensorsSpec);
                return true;
            }
            kitchenNewState = state;
            return false;
        }
        public static bool InstallBathRoomSensors(this IRepository<IDbProperties> repository, BathRoomState state, out BathRoomState bathroomNewState)
        {
            var sensors = state.Sensors;
            var dbProperties = new List<IDbProperties>();
            foreach (var sensor in sensors.ToList())
            {
                var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@bathroom", SqlDbType.NVarChar, 50, state.BathRoomId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, sensor.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@role", SqlDbType.NVarChar, 50, sensor.Role, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@sensorid", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@sensorid")
                        };
                var repos = new DbProperties("InstallBathRoomSensor", dataTypes, string.Empty, true, "@sensorid", sensor.Tag);
                dbProperties.Add(repos);
            }
            var x = repository.Update(dbProperties);
            if (x > 0)
            {
                var sensorsSpec = new List<SensorSpec>();
                foreach (var sensor in sensors.ToList())
                {
                    var outputs = repository.OutPuts;
                    sensor.SensorId = outputs.ToList().FirstOrDefault(y => y.Identifier == sensor.Tag).Value;
                    sensorsSpec.Add(sensor);
                }
                bathroomNewState = new BathRoomState(state.BathRoomId, state.Tag, sensorsSpec);
                return true;
            }
            bathroomNewState = state;
            return false;
        }
        public static bool InstallToiletSensors(this IRepository<IDbProperties> repository, ToiletState state, out ToiletState toiletNewState)
        {
            var sensors = state.Sensors;
            var dbProperties = new List<IDbProperties>();
            foreach (var sensor in sensors.ToList())
            {
                var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@toilet", SqlDbType.NVarChar, 50, state.ToiletId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, sensor.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@role", SqlDbType.NVarChar, 50, sensor.Role, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@sensorid", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@sensorid")
                        };
                var repos = new DbProperties("InstallToiletSensor", dataTypes, string.Empty, true, "@sensorid", sensor.Tag);
                dbProperties.Add(repos);
            }
            var x = repository.Update(dbProperties);
            if (x > 0)
            {
                var sensorsSpec = new List<SensorSpec>();
                foreach (var sensor in sensors.ToList())
                {
                    var outputs = repository.OutPuts;
                    sensor.SensorId = outputs.ToList().FirstOrDefault(y => y.Identifier == sensor.Tag).Value;
                    sensorsSpec.Add(sensor);
                }
                toiletNewState = new ToiletState(state.ToiletId, state.Tag, sensorsSpec);
                return true;
            }
            toiletNewState = state;
            return false;
        }
        public static bool InstallSepticTankSensors(this IRepository<IDbProperties> repository, SepticTankState state, out SepticTankState septicTankState)
        {
            var sensors = state.Sensors;
            var dbProperties = new List<IDbProperties>();
            foreach(var sensor in sensors.ToList())
            {
                var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@septictank", SqlDbType.NVarChar, 50, state.SepticTankId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, sensor.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@role", SqlDbType.NVarChar, 50, sensor.Role, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@sensorid", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@sensorid")
                        };
                var repos = new DbProperties("InstallSepticSensor", dataTypes, string.Empty, true, "@sensorid", sensor.Tag);
                dbProperties.Add(repos);
            }            
            var x = repository.Update(dbProperties);
            if (x > 0)
            {
                var sensorsSpec = new List<SensorSpec>();
                foreach (var sensor in sensors.ToList())
                {
                    var outputs = repository.OutPuts;
                    sensor.SensorId = outputs.ToList().FirstOrDefault(y => y.Identifier == sensor.Tag).Value;
                    sensorsSpec.Add(sensor);
                }
                septicTankState = new SepticTankState(state.SepticTankId, state.Height, state.AlertHeight, sensorsSpec);
                return true;
            }
            septicTankState = state;
            return false;
        }
        public static bool InstallReservoirSensors(this IRepository<IDbProperties> repository, WaterReservoirState state, out WaterReservoirState waterReservoirState)
        {
            var sensors = state.Sensors;
            var dbProperties = new List<IDbProperties>();
            foreach (var sensor in sensors.ToList())
            {
                var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@reservoir", SqlDbType.NVarChar, 50, state.ReservoirId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, sensor.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@role", SqlDbType.NVarChar, 50, sensor.Role, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@sensorid", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@sensorid")
                        };
                var repos = new DbProperties("InstallReservoirSensor", dataTypes, string.Empty, true, "@sensorid", sensor.Tag);
                dbProperties.Add(repos);
            }
            var x = repository.Update(dbProperties);
            if (x > 0)
            {
                var sensorsSpec = new List<SensorSpec>();
                foreach (var sensor in sensors.ToList())
                {
                    var outputs = repository.OutPuts;
                    sensor.SensorId = outputs.ToList().FirstOrDefault(y => y.Identifier == sensor.Tag).Value;
                    sensorsSpec.Add(sensor);
                }
                waterReservoirState = new WaterReservoirState(state.ReservoirId, state.Height, state.AlertHeight, sensorsSpec);
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
                            new DataTypes("@hostel", SqlDbType.NVarChar, 50, spec.HostelId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, septic.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@height", SqlDbType.Int, 0, septic.Height.ToString(), ParameterDirection.Input, false, false, ""),
                            new DataTypes("@septictank", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@septictank")
                        };
            var repos = new DbProperties("CreateSepticTank", dataTypes, string.Empty, true, "@septictank");
            var x = repository.Update(new[] { repos });
            if (x > 0 || !string.IsNullOrWhiteSpace(repos.Id))
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
                            new DataTypes("@hostel", SqlDbType.NVarChar, 50, spec.HostelId, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, water.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@height", SqlDbType.Int, 0, water.Height.ToString(), ParameterDirection.Input, false, false, ""),
                            new DataTypes("@reservoir", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@reservoir")
                        };
            var repos = new DbProperties("CreateReservoir", dataTypes, string.Empty, true, "@reservoir");
            var x = repository.Update(new[] { repos });
            if (x > 0 || !string.IsNullOrWhiteSpace(repos.Id))
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
            if (x > 0 || !string.IsNullOrWhiteSpace(repos.Id))
            {
                hostel.Detail.HostelId = repos.Id;
                return true;
            }
            return false;
        }
    }
}
