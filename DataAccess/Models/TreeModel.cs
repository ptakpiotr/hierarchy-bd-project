using DataAccess.Extensions;
using System.Xml;

namespace DataAccess.Models
{
    public class TreeModel
    {
        public int Id { get; set; }

        public XmlDocument Family { get; set; }

        public static implicit operator TreeDTO(TreeModel t)
        {
            return new()
            {
                Id = t.Id,
                Family = t.Family.ConvertDocumentToType<List<PersonModel>>()
            };
        }
    }
}
