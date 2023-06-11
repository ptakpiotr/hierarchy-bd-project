using System.Collections.Generic;

namespace DataAccess.Models
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public List<EmployeeModel> Company { get; set; }
    }
}
