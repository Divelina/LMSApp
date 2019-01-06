
using LMSApp.Data.Common;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.CommonInterfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using System.Linq;
using System.Collections.Generic;
using LMSApp.Services.Models.Users;
using LMSApp.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LMSApp.Services.DataServices
{
    public class EducatorService : IEducatorService
    {
        private readonly IRepository<Educator> educatorRepository;

        public EducatorService(IRepository<Educator> educatorRepository)
        {
            this.educatorRepository = educatorRepository;
        }

        public Educator GetByUserId(string userId)
        {
            var educator = this.educatorRepository.All()
                .Include(e => e.User)
                .Where(e => e.UserId == userId)
                .FirstOrDefault();

            return educator;             
        }

        //public Educator GetById(string educatorId)
        //{
        //    var educator = this.educatorRepository.All()
        //        .Where(e => e.Id == educatorId)
        //        .FirstOrDefault();

        //    return educator;
        //}

        public IEnumerable<EducatorIdAndNameViewModel> GetAll()
        {
            var courses = this.educatorRepository.All()
                .To<EducatorIdAndNameViewModel>();

            return courses;
        }

    }
}
