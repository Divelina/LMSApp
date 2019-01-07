
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
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using LMSApp.Data.Models;
using System;

namespace LMSApp.Services.DataServices
{
    public class EducatorService : IEducatorService
    {
        private readonly IRepository<Educator> educatorRepository;
        private readonly UserManager<LMSAppUser> userManager;

        public EducatorService(IRepository<Educator> educatorRepository, UserManager<LMSAppUser> userManager)
        {
            this.educatorRepository = educatorRepository;
            this.userManager = userManager;
        }

        public Educator GetByUserId(string userId)
        {
            var educator = this.educatorRepository.All()
                .Include(e => e.User)
                .Where(e => e.UserId == userId)
                .FirstOrDefault();

            return educator;             
        }

        public async Task<string> CreateAsync(EducatorBindingModel educator)
        {

            var newEducator = Mapper.Map<Educator>(educator);

            await this.educatorRepository.AddAsync(newEducator);
            await this.educatorRepository.SaveChangesAsync();

            return newEducator.Id;
        }


        public async Task DeleteEducatorByUserId(string userId)
        {
            var educator = this.educatorRepository.All()
                .Where(ed => ed.UserId == userId && ed.IsDeleted == false)
                 .FirstOrDefault();

            if (educator != null)
            {
                educator.IsDeleted = true;
                educator.DeletedOn = DateTime.UtcNow;
            }

            await this.educatorRepository.SaveChangesAsync();
        }

        public async Task UnDeleteEducatorByUserId(string userId)
        {

            var educator = this.educatorRepository.All()
                .Where(ed => ed.UserId == userId && ed.IsDeleted == true)
                 .FirstOrDefault();

            if (educator != null)
            {
                educator.IsDeleted = false;
                educator.DeletedOn = null;
            }

            await this.educatorRepository.SaveChangesAsync();
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
