using Akka.Actor;
using Hostel.Entity.Handler.Sensor;
using Hostel.Entity.Sensor;
using Hostel.Event;
using Hostel.State.Floor.Units;
using Hostel.State.Sensor;
using Shared;
using Shared.Actors;

namespace Hostel.Entity.Floor.Units
{
    public class KitchenActor: HostelActor<KitchenState>
    {
        private string _connectionString;
        public KitchenActor(ICommandHandler<KitchenState> handler, KitchenState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
        {
            _connectionString = connectionString;
        }
        protected override void PreStart()
        {
            base.PreStart();
        }
        protected override void OnPersist(IEvent persistedEvent)
        {
            switch(persistedEvent)
            {
                case InstalledSensor sensors:
                    {
                        foreach (var sensor in sensors.Sensors)
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
        protected override void OnSnapshotOffer(KitchenState state)
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
        public static Props Prop(ICommandHandler<KitchenState> handler, KitchenState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new KitchenActor(handler, defaultState, persistenceId, connectionString));
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
