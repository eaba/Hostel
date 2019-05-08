using Akka.Actor;
using Hostel.Entity.Floor.Units;
using Hostel.Entity.Handler;
using Hostel.Model;
using Hostel.State.Floor;
using Hostel.State.Floor.Units;
using Shared;
using Shared.Actors;
using System.Collections.Generic;

namespace Hostel.Entity.Floor
{
    public class BathRoomManagerActor : HostelActor<BathRoomManagerState>
    {
        private IEnumerable<BathRoomSpec> _bathSpec;
        private string _connectionString;
        public BathRoomManagerActor(ICommandHandler<BathRoomManagerState> handler, IEnumerable<BathRoomSpec> specs, BathRoomManagerState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
        {
            _connectionString = connectionString;
            _bathSpec = specs;
        }
        protected override void PreStart()
        {
            foreach(var bath in _bathSpec)
            {
                var child = Context.Child(bath.Tag);
                if (child.IsNobody())
                {
                    Context.ActorOf(BathRoomActor.Prop(new BathRoomHandler(), bath.Sensors, BathRoomState.Empty, bath.Tag, _connectionString), bath.Tag);
                }
            }
            base.PreStart();
        }
        public static Props Prop(ICommandHandler<BathRoomManagerState> handler, IEnumerable<BathRoomSpec> specs, BathRoomManagerState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new BathRoomManagerActor(handler, specs, defaultState, persistenceId, connectionString));
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
