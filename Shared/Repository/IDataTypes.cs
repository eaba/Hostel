using System.Data;

namespace Shared.Repository
{
    public interface IDataTypes
    {
        string Parameter { get; }
        SqlDbType DbType { get; }
        int DbLength { get; }
        string Value { get; set; }
        ParameterDirection Direction { get; }
        bool IsOutput { get; }
        bool NeedPrevious { get; }
        string OutParam { get; }
    }
}
