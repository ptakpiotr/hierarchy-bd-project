using DataAccess.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DataAccess.Models
{
    [XmlType(TypeName = "person")]
    public class PersonModel
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [XmlAttribute("firstName")]
        [Required]
        public string FirstName { get; set; } = null!;

        [XmlAttribute("lastName")]
        [Required]
        public string LastName { get; set; } = null!;

        [XmlAttribute("dateOfBirth")]
        [ValidateDateOfBirth]
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;

        [XmlAttribute("gender")]
        [ValidateGender]
        public string Gender { get; set; }

        [XmlAttribute("motherId")]
        public Guid MotherId { get; set; }

        [XmlAttribute("fatherId")]
        public Guid FatherId { get; set; }
    }
}
