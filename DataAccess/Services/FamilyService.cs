using DataAccess.Extensions;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataAccess.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IConfiguration _configuration;

        public FamilyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<TreeDTO>> GetFamilies()
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            List<TreeModel> data = await conn.ReadDataAsync<TreeModel>("Tree").ConfigureAwait(false);
            List<TreeDTO> trees = new List<TreeDTO>();

            foreach (TreeModel t in data)
            {
                trees.Add(t);
            }

            return trees;
        }

        public async Task<TreeDTO> GetFamily(int id)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            List<TreeModel> allData = await conn.ReadDataWithConditionAsync<TreeModel, dynamic>("Tree", "id = @Id", new { Id = id })
                .ConfigureAwait(false);
            TreeModel data = allData.FirstOrDefault();

            return data;
        }

        public async Task InsertFamily(List<PersonModel> people)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            await conn.SaveXMLDataAsync(people, "Tree", "Family");
        }

        public async Task InsertPerson(PersonModel person, int familyId)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            XmlSerializer serializer = new XmlSerializer(typeof(PersonModel));

            using StringWriter writer = new StringWriter();
            serializer.Serialize(writer, person);

            string serializedData = writer.ToString();

            await conn.UpdateXMLDataAsync(serializedData, "Tree", "Family", "/ArrayOfPerson", familyId);
        }

        public async Task UpdateFamily(List<PersonModel> people, int familyId)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            XmlSerializer serializer = new XmlSerializer(typeof(List<PersonModel>));

            using StringWriter writer = new StringWriter();
            serializer.Serialize(writer, people);

            string serializedData = writer.ToString();

            await conn.UpdateXMLDataWrapperAsync(serializedData, "Tree", "Family", familyId);
        }

        public async Task<List<PersonReportOne>> GetReportOneData()
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            Dictionary<string, (string, string)> xPathMappings = new Dictionary<string, (string, string)>()
            {
                {"@id",("UNIQUEIDENTIFIER","Id") },
                {"@firstName",("NVARCHAR(MAX)","FirstName") },
                {"@lastName",("NVARCHAR(MAX)","LastName") },
                {"@gender",("NVARCHAR(2)","Gender") }
            };

            return await conn.ReadXMLValuesAsync<PersonReportOne>("Tree", "/ArrayOfPerson/person",
                xPathMappings, "C.value('@dateOfBirth','DATETIME') > DATEADD(YEAR,-2,GETDATE())");
        }

        public async Task<List<FamilyCountModel>> GetReportTwoData()
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            string query = @"SELECT COUNT(C.value('@id', 'NVARCHAR(MAX)')) as FamilyCount, Id as FamilyId
		                        FROM
			                        Tree
		                        CROSS APPLY
			                        Family.nodes('/ArrayOfPerson/person') AS T(C)
		                        GROUP BY Id";

            return await conn.SelectStandardDataAsync<FamilyCountModel>(query);
        }
    }
}
