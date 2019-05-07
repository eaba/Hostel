using Hostel.Command;
using Hostel.Event;
using Hostel.Repository;
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
                case CreateFloor creatFloor:
                    {
                        var floor = creatFloor;
                        if(repository.CreateFloor(floor, out var id))
                        {
                            return new HandlerResult(new CreatedFloor(new Model.Floor(Guid.Parse(id), floor.Floor.Tag)));
                        }
                        return new HandlerResult($"Floor {floor.Floor.Tag} could not be created at this time!");
                    }
                default: return HandlerResult.NotHandled(command);
            }
        }
    }
}
