
using LMSApp.Data.Common;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.CommonInterfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using System.Linq;
using System.Collections.Generic;
using LMSApp.Services.Models.Users;
using LMSApp.Services.Mapping;
using LMSApp.Data.Models;
using System;
using LMSApp.Data.Models.CourseRelated;

namespace LMSApp.Services.DataServices
{
    public class EducatorLectureciseService : IEducatorLectureciseService
    {
        private readonly IRepository<EducatorLecturecise> educatorLectureciseRepository;

        public EducatorLectureciseService(IRepository<EducatorLecturecise> educatorLectureciseRepository)
        {
            this.educatorLectureciseRepository = educatorLectureciseRepository;
        }

        public async Task<string> DeleteById(string lectureciseId)
        {
            var educatorLecturecises = this.educatorLectureciseRepository.All()
                .Where(el => el.LectureciseId == lectureciseId)
                .ToList();

            var count = educatorLecturecises.Count();

            for (int i = count - 1; i >= 0; i--)
            {
                this.educatorLectureciseRepository.Delete(educatorLecturecises[i]);
            }

            await this.educatorLectureciseRepository.SaveChangesAsync();

            return lectureciseId;
        }


    }
}
