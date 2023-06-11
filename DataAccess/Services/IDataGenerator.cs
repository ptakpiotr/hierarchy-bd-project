using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Services
{
    public interface IDataGenerator
    {
        List<EmployeeModel> GenerateRandomCompany();
        EmployeeModel GenerateRandomEmployee();
        PersonModel GenerateRandomPerson();
    }
}