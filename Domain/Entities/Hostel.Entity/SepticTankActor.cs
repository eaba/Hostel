using Akka.Actor;
using Hostel.Model;
using Hostel.State;
using Shared;
using Shared.Actors;
using System.Collections.Generic;

namespace Hostel.Entity
{
    public class SepticTankActor: HostelActor<SepticTankState>
    {
        private string _connectionString;
        public SepticTankActor(ICommandHandler<SepticTankState> handler, SepticTankState defaultState, string persistenceId, string connectionstring)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionstring))
        {
            _connectionString = connectionstring;
        }
        public static Props Prop(ICommandHandler<SepticTankState> handler, SepticTankState defaultState, string persistenceId, string connectstring)
        {
            return Props.Create(() => new SepticTankActor(handler, defaultState, persistenceId, connectstring));
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
