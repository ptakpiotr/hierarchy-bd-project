using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface ICompanyService
    {
        Task<List<CompanyDTO>> GetCompanies();
        Task<CompanyDTO> GetCompany(int id);
        Task<List<CompanyReportOne>> GetReportOneData(int companyId);
        Task<List<CompanyReportTwo>> GetReportTwoData(int companyId);
        Task InsertEmployee(EmployeeModel employee, int companyId);
        Task InsertEmployees(List<EmployeeModel> employees);
        Task UpdateCompany(EmployeeModel em, int companyId);
    }
}