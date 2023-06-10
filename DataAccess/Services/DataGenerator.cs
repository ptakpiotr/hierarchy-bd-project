using Bogus;
using DataAccess.Models;

namespace DataAccess.Services
{
    public class DataGenerator : IDataGenerator
    {
        private readonly Faker<PersonModel> personFaker = new Faker<PersonModel>()
                                .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                                .RuleFor(p => p.LastName, f => f.Person.LastName)
                                .RuleFor(p => p.DateOfBirth, f => f.Date.Past())
                                .RuleFor(p => p.Gender, f => f.Random.String2(1, "MK"));

        public PersonModel GenerateRandomPerson()
        {
            return personFaker.Generate();
        }
    }
}
