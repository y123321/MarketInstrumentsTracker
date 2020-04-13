using MarketsTracker.Common;
using MarketsTracker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketsTracker.DAL
{
    static class QueryExecuter
    {

        public static async Task ExecuteNoQuery(string sql, string connectionString, params SqlParameter[] sqlParameters)
        {
            Func<SqlCommand, Task<string>> func = async cmd =>
              {
                  await cmd.ExecuteNonQueryAsync();
                  return null;
              };
            await ExecuteSql(sql, connectionString, func, sqlParameters);
        }
        public static async Task<object> ExecuteScalar(string sql, string connectionString, params SqlParameter[] sqlParameters)
        {
            Func<SqlCommand, Task<object>> func = async cmd => await cmd.ExecuteScalarAsync();
            var queryRes = await ExecuteSql(sql, connectionString, func, sqlParameters);
            if (queryRes == DBNull.Value)
                return null;
            return queryRes;
        }
        public static async Task<ICollection<T>> ExecuteCollection<T>(string sql, Func<IDataReader, T> mapperFunc, string connectionString, params SqlParameter[] sqlParameters) where T : new()
        {
            if (mapperFunc == null)
                throw new NullReferenceException($"mapperFunc cant be null. Use {nameof(ExecuteNoQuery)} if you dont want to read data.");
            var res = new List<T>();
            Func<SqlCommand, Task<bool>> func = async cmd =>
             {
                 var reader = await cmd.ExecuteReaderAsync();
                 while (await reader.ReadAsync())
                     res.Add(mapperFunc(reader));
                 return true;
             };
            var reader = await ExecuteSql(sql, connectionString, func, sqlParameters);
            return res;
        }

        public static async Task<T> ExecuteSql<T>(string sql, string connectionString, Func<SqlCommand, Task<T>> executeFunc, params SqlParameter[] sqlParameters)
        {
            using (var con = new SqlConnection(connectionString))
            using (var dbCommand = new SqlCommand(sql, con))
            {
                if (sqlParameters != null && sqlParameters.Any())
                    dbCommand.Parameters.AddRange(sqlParameters);
                await con.OpenAsync();
                var res = await executeFunc(dbCommand);
                return res;
            }

        }

    }
}
