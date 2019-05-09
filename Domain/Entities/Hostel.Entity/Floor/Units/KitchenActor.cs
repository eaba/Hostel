using Akka.Actor;
using Hostel.Model;
using Hostel.State.Floor;
using Hostel.State.Floor.Units;
using Shared;
using Shared.Actors;
using System.Collections.Generic;

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
