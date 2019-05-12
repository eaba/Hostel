using Akka.Actor;
using Hostel.Entity.Handler.Sensor;
using Hostel.Entity.Sensor;
using Hostel.Event;
using Hostel.State;
using Hostel.State.Sensor;
using Shared;
using Shared.Actors;

namespace Hostel.Entity
{ 
    public class WaterReservoirActor: HostelActor<WaterReservoirState>
    {
        private string _connectionString;
        public WaterReservoirActor(ICommandHandler<WaterReservoirState> handler, WaterReservoirState defaultState, string persistenceId, string connectionstring)
            : base(handler, defaultState, persistenceId, connectionstring)
        {
            _connectionString = connectionstring;
        }
        public static Props Prop(ICommandHandler<WaterReservoirState> handler, WaterReservoirState defaultState, string persistenceId, string connectionstring)
        {
            return Props.Create(() => new WaterReservoirActor(handler, defaultState, persistenceId, connectionstring));
        }
        protected override void OnPersist(IEvent persistedEvent)
        {
            switch (persistedEvent)
            {
                case InstalledSensor installedSensor:
                    {
                        foreach (var sensor in installedSensor.Sensors)
                        {
                            if (Context.Child(sensor.Tag).IsNobody())
                            {
                                var sensorState = new SensorState(sensor.SensorId, sensor.Tag, sensor.Role);
                                Context.ActorOf(SensorActor.Prop(new SensorHandler(), sensorState, sensor.Tag, _connectionString), sensor.Tag);
                            }
                        }
                    }
                    break;
            }
            base.OnPersist(persistedEvent);
        }
        protected override void OnSnapshotOffer(WaterReservoirState state)
        {
            var sensors = state.Sensors;
            foreach (var sensor in sensors)
            {
                if (Context.Child(sensor.Tag).IsNobody())
                {
                    var sensorState = new SensorState(sensor.SensorId, sensor.Tag, sensor.Role);
                    Context.ActorOf(SensorActor.Prop(new SensorHandler(), sensorState, sensor.Tag, _connectionString), sensor.Tag);
                }
            }
            base.OnSnapshotOffer(state);
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
