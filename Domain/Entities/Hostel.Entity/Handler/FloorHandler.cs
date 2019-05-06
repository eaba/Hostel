using Hostel.Command;
using Hostel.Event;
using Hostel.State.Floor;
using Shared;
using Shared.Repository;
using Shared.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Data;

namespace Hostel.Entity.Handler
{
    public class FloorHandler : ICommandHandler<FloorState>
    {
        public HandlerResult Handle(FloorState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch(command)
            {
                case CreateFloor createdFloor:
                    {
                        var floor = createdFloor.Floor;
                        var dataTypes = new List<IDataTypes>
                        {
                            new DataTypes("@tag", SqlDbType.NVarChar, 50, floor.Tag, ParameterDirection.Input, false, false, ""),
                            new DataTypes("@floor", SqlDbType.UniqueIdentifier, 0, string.Empty, ParameterDirection.Output, false, false, "@floor")
                        };
                        var repos = new DbProperties("CreateFloor", dataTypes, string.Empty, true, "@floor");
                        var result = repository.Update(new[] { repos });
                        if(result > 0)
                        {
                            return new HandlerResult(new CreatedFloor(new Model.Floor(Guid.Parse(repos.Id), floor.Tag)));
                        }
                        return new HandlerResult($"Floor {floor.Tag} could not be created at this time!");
                    }
                default: return HandlerResult.NotHandled(command);
            }
        }
    }
}
