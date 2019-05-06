using System.Collections.Generic;

namespace Shared.Repository.Impl
{
    public class DbProperties:IDbProperties
    {

        public string StoredProcedureName { get; }

        public List<IDataTypes> ProcedureProps { get; set; }

        public string Id { get; set; }

        public bool Output { get; }

        public string Param { get; }

        public DbProperties(string procName, List<IDataTypes> props, string id, bool output, string param)
        {
            StoredProcedureName = procName;
            ProcedureProps = props;
            Id = id;
            Output = output;
            Param = param;
        }
    }
}
