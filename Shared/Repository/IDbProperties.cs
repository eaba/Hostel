using System.Collections.Generic;

namespace Shared.Repository
{
    public interface IDbProperties
    {
        string StoredProcedureName { get; }
        List<IDataTypes> ProcedureProps { get; set; }
        string Id { get; set; }
        bool Output { get; }
        string Param { get; }
    }
}
