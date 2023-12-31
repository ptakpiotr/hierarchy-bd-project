﻿using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IFamilyService
    {
        Task<List<TreeDTO>> GetFamilies();
        Task<TreeDTO> GetFamily(int id);
        Task InsertFamily(List<PersonModel> people);
        Task InsertPerson(PersonModel person, int familyId);
        Task UpdateFamily(List<PersonModel> people, int familyId);
        Task<List<PersonReportOne>> GetReportOneData();
        Task<List<FamilyCountModel>> GetReportTwoData();
    }
}