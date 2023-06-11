using Bogus;
using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Services
{
    public class DataGenerator : IDataGenerator
    {
        private readonly Faker<PersonModel> personFaker = new Faker<PersonModel>()
                                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                                .RuleFor(p => p.LastName, f => f.Person.LastName)
                                .RuleFor(p => p.DateOfBirth, f => f.Date.Past())
                                .RuleFor(p => p.Gender, f => f.Random.String2(1, "MK"));

        private readonly Faker<EmployeeModel> employeeFaker = new Faker<EmployeeModel>()
                                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                                .RuleFor(p => p.LastName, f => f.Person.LastName);

        public PersonModel GenerateRandomPerson()
        {
            return personFaker.Generate();
        }

        public EmployeeModel GenerateRandomEmployee()
        {
            return employeeFaker.Generate();
        }

        public List<EmployeeModel> GenerateRandomCompany()
        {
            return employeeFaker.GenerateBetween(10, 15);
        }
    }
}
