using DataAccess.Models;
using DataAccess.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DataAccess.Tests
{
    public class CompanyServiceTests
    {
        private readonly CompanyService _companyService;
        private readonly DataGenerator _dataGenerator;

        public CompanyServiceTests()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            _companyService = new CompanyService(config);
            _dataGenerator = new DataGenerator();

            InsertEmployees(10).GetAwaiter().GetResult();
        }

        private List<EmployeeModel> GenerateEmployees(int m)
        {
            var data = new List<EmployeeModel>();

            for (int i = 0; i < m; i++)
            {
                data.Add(_dataGenerator.GenerateRandomEmployee());
            }

            return data;
        }

        private async Task InsertEmployees(int c)
        {
            var e = GenerateEmployees(c);

            if (!(await _companyService.GetCompanies()).Any())
            {
                await _companyService.InsertEmployees(e);
            }
        }

        [Fact]
        public async Task GetCompanies_ShouldReturnData()
        {
            var data = await _companyService.GetCompanies();

            data.Should().NotBeNull().And.HaveCountGreaterThanOrEqualTo(1);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task GetCompany_ShouldNotReturnAnything(int id)
        {
            var data = await _companyService.GetCompany(id);

            data.Should().BeNull();
        }

        [Theory]
        [InlineData(1)]
        public async Task GetReportOneData_ShouldNotBeNull(int id)
        {
            var data = await _companyService.GetReportOneData(id);

            data.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData(221)]
        [InlineData(2211)]
        public async Task GetReportTwoData_ShouldBeNull(int id)
        {
            var data = await _companyService.GetReportTwoData(id);

            data.Should().BeNullOrEmpty();
        }
    }
}
