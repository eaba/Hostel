using Akka.Persistence;

namespace Hostel.Query
{
    public class ManagerQueryActor : ReceivePersistentActor
    {
        public override string PersistenceId { get; }
        public ManagerQueryActor(string persistenceId)
        {
            PersistenceId = Context.Parent.Path.Name+"_Query";
        }
    }
}
