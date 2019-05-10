using Hostel.Command;
using Hostel.Command.Create;
using Hostel.Event;
using Hostel.Event.Created;
using Hostel.Repository;
using Hostel.State;
using Shared;
using Shared.Repository;

namespace Hostel.Entity.Handler
{
    public class HostelManagerHandler : ICommandHandler<HostelManagerState>
    {
        public HandlerResult Handle(HostelManagerState state, ICommand command, IRepository<IDbProperties> repository)
        {
            switch (command)
            {
                case ConstructHostel construct:
                    {
                        var hostel = construct.Construction;
                        if (!state.Constructed)
                        {
                            if (repository.ConstructHostel(hostel))
                            {
                                return new HandlerResult(new ConstructedHostel(hostel));
                            }
                            return new HandlerResult($"Hostel {hostel.Detail.Name} could not be constructed at this time!", string.Empty, string.Empty);
                        }
                        return new HandlerResult($"Hostel {hostel.Detail.Name} alread exist. Did the government demonish your hostel?", string.Empty, string.Empty);
                    }
                case CreateFloor createFloor:
                    {
                        var floor = createFloor.Floor;
                        if (repository.CreateFloor(floor))
                        {
                            return new HandlerResult(new CreatedFloor(floor));
                        }
                        return new HandlerResult($"Floor {floor.Tag} could not be created at this time!", createFloor.Commander, createFloor.CommandId);
                    }
                case CreateSepticTank createSeptic:
                    {
                        var tank = createSeptic.Spec;
                        if (repository.CreateSepticTank(tank))
                        {
                            return new HandlerResult(new CreatedSepticTank(tank));
                        }
                        return new HandlerResult($"SepticTank {tank.Tag} could not be created at this time!", createSeptic.Commander, createSeptic.CommandId);
                    }
                case CreateWaterReservoir createWater:
                    {
                        var water = createWater.Spec;
                        if (repository.CreateReservoir(water))
                        {
                            return new HandlerResult(new CreatedWaterReservoir(water));
                        }
                        return new HandlerResult($"Water Reservoir {water.Tag} could not be created at this time!", createWater.Commander, createWater.CommandId);
                    }
                default: return HandlerResult.NotHandled(command, command.Commander, command.CommandId);
            }
        }
    }
}
