using Dapper;
using System.Data;

namespace DataAccess.Extensions
{
    public static class DapperStandardExtensions
    {
        public static async Task InsertStandardDataAsync<T>(this IDbConnection conn, T data, string tableName, params string[]? columns) where T : class
        {
            string[]? columnNames = columns;

            if (columnNames is null)
            {
                columnNames = typeof(T).GetProperties().Select(p => p.Name).Where(v => v != "Id").ToArray();
            }

            string insertCols = string.Join(",", columnNames);

            await conn.ExecuteAsync($"INSERT INTO {tableName}({insertCols}) VALUES(@data)", new { data });
        }
    }
}
