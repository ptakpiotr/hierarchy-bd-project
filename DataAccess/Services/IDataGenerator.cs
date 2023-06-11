using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Services
{
    public interface IDataGenerator
    {
        List<EmployeeModel> GenerateRandomCompany(int min = 10, int max = 15);
        EmployeeModel GenerateRandomEmployee();
        PersonModel GenerateRandomPerson();
    }
}