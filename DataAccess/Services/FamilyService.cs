using DataAccess.Extensions;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

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

            List<TreeModel> data = await conn.ReadXMLDataAsync<TreeModel>("Tree").ConfigureAwait(false);
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

            List<TreeModel> allData = await conn.ReadXMLDataWithConditionAsync<TreeModel, dynamic>("Tree", "id = @Id", new { Id = id })
                .ConfigureAwait(false);
            TreeModel data = allData.FirstOrDefault();

            return data;
        }

        public async Task InsertFamily(List<PersonModel> people)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            await conn.SaveXMLDataAsync(people, "Tree", "Family");
        }

        public async Task RemoveElement(Guid personId)
        {

        }
    }
}
