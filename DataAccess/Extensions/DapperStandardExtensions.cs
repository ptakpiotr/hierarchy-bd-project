using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
    public static class DapperStandardExtensions
    {
        public static async Task InsertStandardDataAsync<T>(this IDbConnection conn, T data, string tableName, params string[]? columns) where T : class
        {
            string[]? columnNames = columns;

            if (columnNames is null || !columnNames.Any())
            {
                columnNames = typeof(T).GetProperties().Select(p => p.Name).Where(v => v != "Id").ToArray();
            }

            string insertCols = string.Join(",", columnNames.Select(c => c));
            string insertValues = string.Join(",", columnNames.Select(c => $@"'{typeof(T).GetProperty(c).GetValue(data)
                .ToString().Replace("'", "")}'"));

            string query = $"INSERT INTO {tableName}({insertCols}) VALUES({insertValues})";

            await conn.ExecuteAsync(query);
        }

        public static async Task<List<T>> ExecuteFunctionAsync<T, U>(this IDbConnection conn, string functionName, U parameter)
        {
            string query = $"SELECT * FROM {functionName}({parameter})";

            IEnumerable<T> data = await conn.QueryAsync<T>(query);

            return data.ToList();
        }

        public static async Task<List<T>> SelectStandardDataAsync<T>(this IDbConnection conn, string query) where T : class
        {
            IEnumerable<T> data = await conn.QueryAsync<T>(query);

            return data.ToList();
        }
    }
}
