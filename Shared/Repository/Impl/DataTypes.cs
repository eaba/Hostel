using System;
using System.Data;

namespace Shared.Repository.Impl
{
    public class DataTypes : IDataTypes
    {
        public string Parameter { get; }

        public SqlDbType DbType { get; }

        public int DbLength { get; }

        public string Value { get; set; }

        public ParameterDirection Direction { get; }

        public bool IsOutput { get; }

        public bool NeedPrevious { get; }

        public string OutParam { get; }

        public DataTypes(string param, SqlDbType sqlDbType, int length, string value, ParameterDirection direction, bool output, bool needprevious, string outputParams)
        {
            Parameter = param;
            DbType = sqlDbType;
            DbLength = length;
            Value = value;
            Direction = direction;
            IsOutput = output;
            NeedPrevious = needprevious;
            OutParam = outputParams;
        }
    }

    public class OutPut
    {
        public string Param { get; }
        public string Value { get; }
        public string Identifier { get; }
        public OutPut(string param, string value, string identifier)
        {
            Param = param;
            Value = value;
            Identifier = identifier;
        }
    }
}
