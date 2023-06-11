using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DataAccess.Models
{
    [XmlType(TypeName = "employee")]
    public class EmployeeModel
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [XmlAttribute("firstName")]
        [Required]
        public string FirstName { get; set; } = null!;

        [XmlAttribute("lastName")]
        [Required]
        public string LastName { get; set; } = null!;

        [XmlAttribute("bossId")]
        public Guid BossId { get; set; }

        [XmlAttribute("post")]
        public string Post { get; set; }
    }
}
