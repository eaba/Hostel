using Akka.Actor;
using Hostel.Model;
using Hostel.State;
using Shared;
using Shared.Actors;
using System.Collections.Generic;

namespace Hostel.Entity
{ 
    public class WaterReservoirActor: HostelActor<WaterReservoirState>
    {
        private IEnumerable<SensorSpec> _sensors;
        private string _connectionString;
        public WaterReservoirActor(ICommandHandler<WaterReservoirState> handler, IEnumerable<SensorSpec> sensors, WaterReservoirState defaultState, string persistenceId, string connectionstring)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionstring))
        {
            _connectionString = connectionstring;
            _sensors = sensors;
        }
        public static Props Prop(ICommandHandler<WaterReservoirState> handler, IEnumerable<SensorSpec> sensors, WaterReservoirState defaultState, string persistenceId, string connectionstring)
        {
            return Props.Create(() => new WaterReservoirActor(handler, sensors, defaultState, persistenceId, connectionstring));
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
