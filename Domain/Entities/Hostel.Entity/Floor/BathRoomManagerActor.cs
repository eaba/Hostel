using Akka.Actor;
using Hostel.State.Floor;
using Shared;
using Shared.Actors;

namespace Hostel.Entity.Floor
{
    public class BathRoomManagerActor : HostelActor<BathRoomManagerState>
    {
        private string _connectionString;
        public BathRoomManagerActor(ICommandHandler<BathRoomManagerState> handler, BathRoomManagerState defaultState, string persistenceId, string connectionString)
            : base(handler, defaultState, persistenceId, new Shared.Repository.Impl.Repository(connectionString))
        {
            _connectionString = connectionString;
        }
        protected override void PreStart()
        {
            /*var child = Context.Child(bath.Tag);
            if (child.IsNobody())
            {
                Context.ActorOf(BathRoomActor.Prop(new BathRoomHandler(), bath.Sensors, BathRoomState.Empty, bath.Tag, _connectionString), bath.Tag);
            }*/
            base.PreStart();
        }
        public static Props Prop(ICommandHandler<BathRoomManagerState> handler, BathRoomManagerState defaultState, string persistenceId, string connectionString)
        {
            return Props.Create(() => new BathRoomManagerActor(handler, defaultState, persistenceId, connectionString));
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
