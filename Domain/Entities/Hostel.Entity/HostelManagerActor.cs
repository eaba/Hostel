using Akka.Actor;
using Akka.Extension;
using Hostel.Command;
using Hostel.Command.Create;
using Hostel.Command.Internal;
using Hostel.Entity.Floor;
using Hostel.Entity.Handler;
using Hostel.Event;
using Hostel.Event.Created;
using Hostel.State;
using Hostel.State.Floor;
using MassTransit;
using Shared;
using Shared.Actors;
using Microsoft.Extensions.DependencyInjection;
using System;
using MassTransit.Event;

namespace Hostel.Entity
{
    public class HostelManagerActor:HostelActor<HostelManagerState>
    {
        private string _connectionString;
        public ISendEndpoint _sendEndPoint;

        public HostelManagerActor(ICommandHandler<HostelManagerState> handler, HostelManagerState defaultState, string persistenceId, string connectionstring)
            : base(handler, defaultState, persistenceId, connectionstring)
        {
            _connectionString = connectionstring;
            Command<IMassTransitCommand>(command => { PrepareCommand(command); });
        }
        protected override void OnRecoverComplete()
        {
            base.OnRecoverComplete();
        }
        protected override void OnSnapshotOffer(HostelManagerState state)
        {
            Self.Tell(new ConstructHostel(state.ConstructionRecord));
            base.OnSnapshotOffer(state);
        }
        protected override void NotifyUI(ICommand command, HandlerResult result)
        {
            var mEvent = new MassTransitEvent(command.Commander, command.CommandId, result);
            PustToUIQueue(mEvent, command.ReplyToQueue);
            base.NotifyUI(command, result);
        }
        private void PustToUIQueue(IMassTransitEvent @event, string queue)
        {
            using (var context = Context.CreateScope())
            {
                var busControl = context.ServiceProvider.GetService<IBusControl>();
                _sendEndPoint = busControl.GetSendEndpoint(new Uri(queue)).ConfigureAwait(false).GetAwaiter().GetResult();//ConfigureAwait(false) to Prevent Deadlock[https://msdn.microsoft.com/en-us/magazine/mt238404.aspx]
                if (_sendEndPoint != null)
                {
                    foreach (var me in State.PendingResponses)
                    {
                        _sendEndPoint.Send(me);
                    }
                    State.PendingResponses.Clear();
                    _sendEndPoint.Send(@event);
                }
                else
                {
                    State.PendingResponses.Add(@event);
                }
            }
        }
        protected override void OnPersist(IEvent persistedEvent, string commandid)
        {
            switch(persistedEvent)
            {
                case ConstructedHostel hostel:
                    {
                        var construct = hostel.Construction;
                        foreach (var floor in construct.Floors)
                        {
                            if(Context.Child(floor.Tag).IsNobody())
                            {
                                floor.HostelId = hostel.Construction.Detail.HostelId;
                                var createFloor = new CreateFloor(floor, hostel.Commander, hostel.CommandId);
                                Self.Tell(createFloor);
                                Context.System.Log.Info($"FloorActor", $"Creating Floor - {floor.Tag}");
                            }
                        }
                        if(Context.Child(construct.SepticTank.Tag).IsNobody())
                        {
                            var septicSpec = construct.SepticTank;
                            septicSpec.HostelId = hostel.Construction.Detail.HostelId;
                            var septic = new CreateSepticTank(septicSpec, hostel.Commander, hostel.CommandId);
                            Self.Tell(septic);
                        }
                        if(Context.Child(construct.Reservoir.Tag).IsNobody())
                        {
                            var waterSpec = construct.Reservoir;
                            waterSpec.HostelId = hostel.Construction.Detail.HostelId; ;
                            var water = new CreateWaterReservoir(waterSpec, hostel.Commander, hostel.CommandId);
                            Self.Tell(water);
                        }
                    }
                    break;
                case CreatedFloor createdFloor:
                    {
                        var floor = createdFloor.Floor;
                        floor.HostelId = State.ConstructionRecord.Detail.HostelId;
                        var floorState = new FloorState(floor);
                        var floorActor = Context.ActorOf(FloorActor.Prop(new FloorHandler(), floorState, floor.Tag, _connectionString), floor.Tag);
                        floorActor.Tell(new LayoutFloor());
                    }
                    break;
                case CreatedSepticTank createdSepticTank:
                    {
                        var septic = createdSepticTank.SepticTankSpec;
                        septic.HostelId = State.ConstructionRecord.Detail.HostelId;
                        var septicState = new SepticTankState(septic.SepticTankId, septic.Height, septic.AlertHeight, septic.Sensors);
                        var septicActor = Context.ActorOf(SepticTankActor.Prop(new SepticTankHandler(), septicState, septic.Tag, _connectionString), septic.Tag);
                        septicActor.Tell(new InstallSensor());
                    }
                    break;
                case CreatedWaterReservoir createdWater:
                    {
                        var water = createdWater.ReservoirSpec;
                        water.HostelId = State.ConstructionRecord.Detail.HostelId;
                        var reservoirState = new WaterReservoirState(water.ReservoirId, water.Height, water.AlertHeight, water.Sensors);
                        var waterActor = Context.ActorOf(WaterReservoirActor.Prop(new WaterReservoirHandler(), reservoirState, water.Tag, _connectionString), water.Tag);
                        waterActor.Tell(new InstallSensor());
                    }
                    break;
            }
            base.OnPersist(persistedEvent, commandid);
        }
        public static Props Prop(ICommandHandler<HostelManagerState> handler, HostelManagerState defaultState, string persistenceId, string connectionstring)
        {
            return Props.Create(() => new HostelManagerActor(handler, defaultState, persistenceId, connectionstring));
        }
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(maxNrOfRetries: 100, withinTimeMilliseconds: 1000, loggingEnabled: true,
                decider: Decider.From(x =>
                {
                    return Directive.Restart;
                }));
        }
        private void PrepareCommand(IMassTransitCommand command)
        {
            switch(command.Command.ToLower())
            {
                case "createperson":
                    {
                        var person = new CreatePerson(command.ReplyToQueue, command.Commander, command.CommandId, command.Payload);
                        Self.Tell(person);
                    }
                    break;
            }
        }
    }
}
