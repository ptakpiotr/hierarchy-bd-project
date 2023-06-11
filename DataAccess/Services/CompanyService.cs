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
    public class CompanyService : ICompanyService
    {
        private readonly IConfiguration _configuration;

        public CompanyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<List<CompanyDTO>> GetCompanies()
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            List<CompanyModel> data = await conn.ReadDataAsync<CompanyModel>("Company").ConfigureAwait(false);
            List<CompanyDTO> companies = new List<CompanyDTO>();

            foreach (CompanyModel c in data)
            {
                companies.Add(c);
            }

            return companies;
        }

        public async Task<CompanyDTO> GetCompany(int id)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            List<CompanyModel> allData = await conn.ReadDataWithConditionAsync<CompanyModel, dynamic>("Company", "id = @Id", new { Id = id })
                .ConfigureAwait(false);
            CompanyModel data = allData.FirstOrDefault();

            return data;
        }

        public async Task InsertEmployees(List<EmployeeModel> employees)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            await conn.SaveXMLDataAsync(employees, "Company", "Company");
        }

        public async Task InsertEmployee(EmployeeModel employee, int companyId)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            XmlSerializer serializer = new XmlSerializer(typeof(EmployeeModel));

            using StringWriter writer = new StringWriter();
            serializer.Serialize(writer, employee);

            string serializedData = writer.ToString();

            await conn.UpdateXMLDataAsync(serializedData, "Company", "Company", "/ArrayOfEmployee", companyId);
        }

        public async Task UpdateCompany(List<EmployeeModel> employees, int companyId)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            XmlSerializer serializer = new XmlSerializer(typeof(List<EmployeeModel>));

            using StringWriter writer = new StringWriter();
            serializer.Serialize(writer, employees);

            string serializedData = writer.ToString();

            await conn.UpdateXMLDataWrapperAsync(serializedData, "Company", "Company", companyId);
        }

        public async Task<List<CompanyReportOne>> GetReportOneData(int companyId)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            return await conn.ExecuteFunctionAsync<CompanyReportOne, int>("fn_Get_Hierarchy_Of_Employees", companyId);
        }

        public async Task<List<CompanyReportTwo>> GetReportTwoData(int companyId)
        {
            using IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("MainConn"));

            Dictionary<string, (string, string)> xPathMappings = new Dictionary<string, (string, string)>()
            {
                {"@firstName",("NVARCHAR(MAX)","FirstName") },
                {"@lastName",("NVARCHAR(MAX)","LastName") }
            };

            return await conn.ReadXMLValuesAsync<CompanyReportTwo>("Company", "/ArrayOfEmployee/employee",
                xPathMappings, $"Id = {companyId}");
        }
    }
}
