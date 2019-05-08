using Akka.Actor;
using Hostel.Entity.Handler.Sensor;
using Hostel.Entity.Sensor;
using Hostel.Model;
using Hostel.State.Floor.Units;
using Hostel.State.Sensor;
using Shared;
using Shared.Actors;
using System.Collections.Generic;

namespace Hostel.Entity.Floor.Units
{
    public class BathRoomActor : HostelActor<BathRoomState>
    {
        private IEnumerable<SensorSpec> _sensors;
        private string _connectionString;
        public BathRoomActor(ICommandHandler<BathRoomState> handler, IEnumerable<SensorSpec> specs, BathRoomState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
        {
            _connectionString = connectionString;
            _sensors = specs;
        }
        protected override void PreStart()
        {
            foreach (var sensor in _sensors)
            {
                var child = Context.Child(sensor.Tag);
                if (child.IsNobody())
                {
                    Context.ActorOf(SensorActor.Prop(new SensorHandler(), SensorState.Empty, sensor.Tag, _connectionString), sensor.Tag);
                }
            }
            base.PreStart();
        }
        public static Props Prop(ICommandHandler<BathRoomState> handler, IEnumerable<SensorSpec> specs, BathRoomState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new BathRoomActor(handler, specs, defaultState, persistenceId, connectionString));
        }
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(maxNrOfRetries: 100, withinTimeMilliseconds: 1000, loggingEnabled: true,
                decider: Decider.From(x =>
                {
                    return Directive.Restart;
                }));
        }
    }
}
