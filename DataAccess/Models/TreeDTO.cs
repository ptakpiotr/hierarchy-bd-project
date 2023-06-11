using System.Collections.Generic;

namespace DataAccess.Models
{
    public class TreeDTO
    {
        public int Id { get; set; }

        public List<PersonModel> Family { get; set; }
    }
}
