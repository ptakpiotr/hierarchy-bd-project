using DataAccess.Models;

namespace DataAccess.Services
{
    public interface IDataGenerator
    {
        PersonModel GenerateRandomPerson();
    }
}