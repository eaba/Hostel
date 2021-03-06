﻿using Akka.Actor;
using Hostel.Command;
using Hostel.Entity.Handler.Sensor;
using Hostel.Entity.Sensor;
using Hostel.Event;
using Hostel.State;
using Hostel.State.Sensor;
using Shared;
using Shared.Actors;

namespace Hostel.Entity
{
    public class SepticTankActor: HostelActor<SepticTankState>
    {
        private string _connectionString;
        public SepticTankActor(ICommandHandler<SepticTankState> handler, SepticTankState defaultState, string persistenceId, string connectionstring)
            : base(handler, defaultState, persistenceId, connectionstring)
        {
            _connectionString = connectionstring;
        }
        public static Props Prop(ICommandHandler<SepticTankState> handler, SepticTankState defaultState, string persistenceId, string connectstring)
        {
            return Props.Create(() => new SepticTankActor(handler, defaultState, persistenceId, connectstring));
        }
        protected override void OnPersist(IEvent persistedEvent, string commandid)
        {
            switch(persistedEvent)
            {
                case InstalledSensor installedSensor:
                    {
                        foreach(var sensor in installedSensor.Sensors)
                        {
                            if(Context.Child(sensor.Tag).IsNobody())
                            {
                                var sensorState = new SensorState(sensor.SensorId, sensor.Tag, sensor.Role);
                                Context.ActorOf(SensorActor.Prop(new SensorHandler(), sensorState, sensor.Tag, _connectionString), sensor.Tag);
                            }
                        }
                    }
                    break;
            }
            base.OnPersist(persistedEvent, commandid);
        }
        protected override void OnSnapshotOffer(SepticTankState state)
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
