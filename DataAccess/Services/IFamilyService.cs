using DataAccess.Models;

namespace DataAccess.Services
{
    public interface IFamilyService
    {
        Task<List<TreeDTO>> GetFamilies();
        Task<TreeDTO> GetFamily(int id);
        Task InsertFamily(List<PersonModel> people);
        Task InsertPerson(PersonModel person, int familyId);
    }
}