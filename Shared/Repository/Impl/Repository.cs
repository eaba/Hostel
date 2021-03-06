using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Shared.Repository.Impl
{
    public class Repository : IRepository<IDbProperties>, IDisposable
    {
        private SqlConnection _connection;
        private bool _disposed;
        public List<OutPut> OutPuts { get; }
        private readonly List<int> _success;
        public Repository(SqlConnection connection)
        {
            OutPuts = new List<OutPut>();
            _connection = connection;
            _success = new List<int>();
        }              
        public IEnumerable<Dictionary<string, string>> Read(IDbProperties properties)
        {
            try
            {
                var cmd = new SqlCommand(properties.StoredProcedureName, _connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddRange(Parameters(properties));
                var x = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
                return GetRows(x);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int Update(IEnumerable<IDbProperties> properties)
        {
            var prop = properties.ToList();
            if (prop.Count > 1)
            {
                return TranNonQuery(properties);
            }
            return NonQuery(prop.FirstOrDefault());
        }
        private IEnumerable<Dictionary<string, string>> GetRows(DbDataReader reader)
        {
            var collectDict = new List<Dictionary<string, string>>();
            if (!reader.HasRows) return collectDict;
            while (reader.Read())
            {
                var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    dictionary.Add(reader.GetName(i), reader.GetValue(i).ToString());
                }
                collectDict.Add(dictionary);
            }
            return collectDict;
        }
        private int TranNonQuery(IEnumerable<IDbProperties> properties)
        {
            try
            {
                var tran = _connection.BeginTransaction();
                foreach (var property in properties.ToList())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(property.StoredProcedureName, _connection, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(Parameters(TransformProps(property)));
                            var rowsAffected = cmd.ExecuteNonQuery();
                            if (property.Output)
                            {
                                var outPutParams = property.Param.Split(',');
                                foreach (var outPutParam in outPutParams)
                                {
                                    var outputValue = cmd.Parameters[outPutParam].Value.ToString();
                                    OutPuts.Add(new OutPut(outPutParam, outputValue, property.Identifier));
                                }
                            }
                            if (rowsAffected >= 0)
                                _success.Add(rowsAffected);
                        }
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        Console.WriteLine(property.StoredProcedureName);
                        Console.WriteLine(e.ToString());
                        throw e;
                    }
                }
                tran.Commit();
                return _success.Sum();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private int NonQuery(IDbProperties prop)
        {
            try
            {
                var cmd = new SqlCommand(prop.StoredProcedureName, _connection)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddRange(Parameters(prop));
                var x = cmd.ExecuteNonQuery();
                if (prop.Output)
                {
                    var outPutParams = prop.Param.Split(',');
                    if (outPutParams.Count() > 1)
                    {
                        foreach (var outPutParam in outPutParams)
                        {
                            var outputValue = cmd.Parameters[outPutParam].Value.ToString();
                            OutPuts.Add(new OutPut(outPutParam, outputValue, prop.Identifier));
                        }
                    }
                    else
                    {
                        prop.Id = cmd.Parameters[prop.Param].Value.ToString();
                    }
                }
                return x;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static SqlParameter[] Parameters(IDbProperties dbProperties)
        {
            var param = new List<SqlParameter>();
            foreach (var db in dbProperties.ProcedureProps)
            {
                var pa = new SqlParameter
                {
                    ParameterName = db.Parameter,
                    SqlDbType = db.DbType,
                    Direction = db.Direction,
                    Value = db.Value
                };
                param.Add(pa);
            }
            return param.ToArray();
        }
        private IDbProperties TransformProps(IDbProperties properties)
        {
            foreach (var dataType in properties.ProcedureProps.ToList())
            {
                if (dataType.NeedPrevious)
                {
                    var outPut = OutPuts.LastOrDefault(x => x.Param == dataType.OutParam);
                    if (outPut != null)
                    {
                        dataType.Value = outPut.Value;
                    }
                }
                //props.ProcedureProps.Add(p);
            }
            return properties;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (!disposing) return;
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
            _disposed = true;
        }

        public void Close()
        {
            _connection.Close();
            _connection.Dispose();
            Dispose();
        }
    }
}
