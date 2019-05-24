using Akka.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

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
