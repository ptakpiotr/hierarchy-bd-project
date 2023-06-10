using Dapper;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace DataAccess.Extensions
{
    public static class DapperXMLExtensions
    {
        public static async Task<List<T>> ReadXMLValueAsync<T>(this IDbConnection conn, string tableName, string xPathQueryRetrieval, string xPathQueryNode, string dbType, string? condition = default)
        {
            string query = @$"SELECT C.value('{xPathQueryRetrieval}', '{dbType}')
                                FROM
                                    {tableName}
                                CROSS APPLY
                                    Family.nodes('{xPathQueryNode}') AS T(C)
                                {(!string.IsNullOrEmpty(condition) ? $"WHERE {condition}" : string.Empty)}
            ";

            IEnumerable<T> res = await conn.QueryAsync<T>(query).ConfigureAwait(false);

            return res.ToList();
        }

        public static async Task<List<T>> ReadDataAsync<T>(this IDbConnection conn, string tableName)
        {
            IEnumerable<T> res = await conn.QueryAsync<T>($"SELECT * FROM {tableName};").ConfigureAwait(false);

            return res.ToList();
        }

        public static async Task<List<T>> ReadDataWithConditionAsync<T, U>(this IDbConnection conn, string tableName, string condition, U conditionObj)
        {
            IEnumerable<T> res = await conn.QueryAsync<T>($"SELECT * FROM {tableName} WHERE {condition};", conditionObj)
                .ConfigureAwait(false);

            return res.ToList();
        }

        public static async Task SaveXMLDataAsync<T>(this IDbConnection conn, T data, string tableName, string columnName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using StringWriter writer = new StringWriter();
            serializer.Serialize(writer, data);

            string serializedData = writer.ToString();

            await conn.ExecuteAsync($"INSERT INTO {tableName}({columnName}) VALUES(@InsertData)", new { InsertData = serializedData });
        }

        public static async Task UpdateXMLDataAsync<T>(this IDbConnection conn, T data, string tableName, string columnName, string xQueryPath, int id)
        {
            string xmlModify = $"insert sql:variable(\"@data\") as last into ({xQueryPath})[1]";
            string query = @$"UPDATE {tableName} SET {columnName}.modify('{xmlModify}') WHERE Id = {id};";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@data", data, DbType.Xml);

            await conn.ExecuteAsync(query, parameters);
        }

        public static async Task UpdateXMLDataWrapperAsync<T>(this IDbConnection conn, T data, string tableName, string columnName, int id)
        {
            string query = @$"UPDATE {tableName} SET {columnName} = @data WHERE Id = {id};";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@data", data, DbType.Xml);

            await conn.ExecuteAsync(query, parameters);
        }

        public static T? ConvertDocumentToType<T>(this XmlDocument doc)
        {
            T? res = default;

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using XmlReader reader = new XmlNodeReader(doc);

            if (serializer.CanDeserialize(reader))
            {
                res = (T?)serializer.Deserialize(reader);
            }

            return res;
        }
    }
}
