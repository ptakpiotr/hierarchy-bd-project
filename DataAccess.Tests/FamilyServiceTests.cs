using DataAccess.Models;
using DataAccess.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DataAccess.Tests
{
    public class FamilyServiceTests
    {
        private readonly FamilyService _familyService;
        private readonly DataGenerator _dataGenerator;

        public FamilyServiceTests()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            _familyService = new FamilyService(config);
            _dataGenerator = new DataGenerator();

            InsertPeople(10).GetAwaiter().GetResult();
        }

        private List<PersonModel> GeneratePeople(int m)
        {
            var data = new List<PersonModel>();

            for (int i = 0; i < m; i++)
            {
                data.Add(_dataGenerator.GenerateRandomPerson());
            }

            return data;
        }

        private async Task InsertPeople(int c)
        {
            var e = GeneratePeople(c);

            if (!(await _familyService.GetFamilies()).Any())
            {
                await _familyService.InsertFamily(e);
            }
        }

        [Fact]
        public async Task GetFamilies_ShouldReturnData()
        {
            var data = await _familyService.GetFamilies();

            data.Should().NotBeNull().And.HaveCountGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task GetReportOneData_ShouldNotBeNull()
        {
            var data = await _familyService.GetReportOneData();

            data.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetReportTwoData_ShouldNotBeNull()
        {
            var data = await _familyService.GetReportTwoData();

            data.Should().NotBeNullOrEmpty();
        }
    }
}
