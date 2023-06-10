﻿using DataAccess.Models;

namespace DataAccess.Services
{
    public interface IFamilyService
    {
        Task<List<TreeDTO>> GetFamilies();
        Task InsertFamily(List<PersonModel> people);
        Task InsertPerson(PersonModel person, int familyId);
    }
}