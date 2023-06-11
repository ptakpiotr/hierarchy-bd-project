using DataAccess.Models;
using DataAccess.Services;
using FluentAssertions;
using Xunit;

namespace DataAccess.Tests
{
    public class DataGeneratorTests
    {
        private readonly DataGenerator _dataGenerator;

        public DataGeneratorTests()
        {
            _dataGenerator = new DataGenerator();
        }

        [Fact]
        public void GeneratingPerson_ShouldNotBeNull()
        {
            PersonModel pm = _dataGenerator.GenerateRandomPerson();

            pm.Should().NotBeNull();
        }

        [Fact]
        public void GeneratingEmployee_ShouldNotBeNull()
        {
            EmployeeModel em = _dataGenerator.GenerateRandomEmployee();

            em.Should().NotBeNull();
        }

        [Theory]
        [InlineData(5, 10)]
        [InlineData(50, 100)]
        [InlineData(7, 14)]
        public void GeneratingRandomCompany_ShouldReturnProperNumber(int min, int max)
        {
            var cm = _dataGenerator.GenerateRandomCompany(min, max);

            cm.Count.Should().BeGreaterThanOrEqualTo(min).And.BeLessThanOrEqualTo(max);
        }
    }
}
