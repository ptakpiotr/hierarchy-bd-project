using Dapper;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace DataAccess.Extensions
{
    public static class DapperXMLExtensions
    {
        public static async Task<List<T>> ReadXMLDataAsync<T>(this IDbConnection conn, string tableName)
        {
            IEnumerable<T> res = await conn.QueryAsync<T>($"SELECT * FROM {tableName};").ConfigureAwait(false);

            return res.ToList();
        }

        public static async Task<List<T>> ReadXMLDataWithConditionAsync<T, U>(this IDbConnection conn, string tableName, string condition, U conditionObj)
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
