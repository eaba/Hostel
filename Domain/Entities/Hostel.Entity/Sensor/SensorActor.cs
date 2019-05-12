using Akka.Actor;
using Hostel.State.Sensor;
using Shared;
using Shared.Actors;

namespace Hostel.Entity.Sensor
{
    public class SensorActor:HostelActor<SensorState>
    {
        public SensorActor(ICommandHandler<SensorState> handler, SensorState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, connectionString)
        {
        }
        public static Props Prop(ICommandHandler<SensorState> handler, SensorState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new SensorActor(handler, defaultState, persistenceId, connectionString));
        }
        
    }
}
