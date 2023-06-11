using DataAccess.Extensions;
using System.Collections.Generic;
using System.Xml;

namespace DataAccess.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }

        public XmlDocument Company { get; set; }

        public static implicit operator CompanyDTO(CompanyModel t)
        {
            return new()
            {
                Id = t.Id,
                Company = t.Company.ConvertDocumentToType<List<EmployeeModel>>()
            };
        }
    }
}
