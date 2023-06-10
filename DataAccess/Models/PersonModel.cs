using System.Xml.Serialization;

namespace DataAccess.Models
{
    [XmlType(TypeName = "person")]
    public class PersonModel
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [XmlAttribute("firstName")]
        public string FirstName { get; set; } = null!;

        [XmlAttribute("lastName")]
        public string LastName { get; set; } = null!;

        [XmlAttribute("dateOfBirth")]
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;

        [XmlAttribute("motherId")]
        public Guid MotherId { get; set; }

        [XmlAttribute("fatherId")]
        public Guid FatherId { get; set; }
    }
}
