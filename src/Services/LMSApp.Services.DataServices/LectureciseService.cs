
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LMSApp.Data.Common;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Mapping;
using LMSApp.Services.Models.Courses;
using Microsoft.EntityFrameworkCore;

namespace LMSApp.Services.DataServices
{
    public class LectureciseService : ILectureciseService
    {
        private readonly IRepository<Lecturecise> lectureciseRepository;

        public LectureciseService(IRepository<Lecturecise> lectureciseRepository)
        {
            this.lectureciseRepository = lectureciseRepository;
        }

        public bool Any(string id)
        {
            var isFound = this.lectureciseRepository.All().Any(l =>
                 l.Id == id && l.IsDeleted == false);

            return isFound;
        }

        public IEnumerable<LectureciseShortViewModel> GetAll()
        {
            var lecturecises = this.lectureciseRepository.All()
                .Where(l => l.IsDeleted == false)
                .To<LectureciseShortViewModel>();

            return lecturecises;
        }

        public IEnumerable<LectureciseShortViewModel> GetByCourseId(string courseId)
        {
            var lecturecises = this.lectureciseRepository.All()
                .Where(l => l.CourseId == courseId && l.IsDeleted == false)
                .To<LectureciseShortViewModel>();

            return lecturecises;
        }


        public async Task<LectureciseDetailsViewModel> GetById(string lectureciseId)
        {
            var lecturecise = await this.lectureciseRepository.FindbyId(lectureciseId);

            if (lecturecise != null && lecturecise.IsDeleted == false)
            {
                var lectureciseModel = this.lectureciseRepository.All()
                    .Where(l => l.Id == lectureciseId)
                    .To<LectureciseDetailsViewModel>()
                    .FirstOrDefault();

                return lectureciseModel;
            }

            return null;
        }

        public Lecturecise GetByOriginal(string lectureciseId)
        {
            var lecturecise = this.lectureciseRepository.All()
                .Include(l => l.LectureciseStudents)
                .Where(l => l.Id == lectureciseId)
                .FirstOrDefault();

            if (lecturecise != null)
            { 
                return lecturecise;
            }

            return null;
        }


        public async Task EditLecturecise(LectureciseDetailsViewModel lectureciseModel)
        {
            var isFound = this.Any(lectureciseModel.Id);

            if (isFound)
            {
                var newLecturecise = Mapper.Map<Lecturecise>(lectureciseModel);

                this.lectureciseRepository.Edit(newLecturecise);

                await this.lectureciseRepository.SaveChangesAsync();
            }
        }

        public async Task SaveLecturecises()
        {
           await this.lectureciseRepository.SaveChangesAsync();
        }

        //Admin only, Accessible from CourseController since Lecturecises are always added to a course
        public async Task<string> CreateAsync(LectureciseCreateBindingModel lecturecise)
        {

            var newLecturecise = Mapper.Map<Lecturecise>(lecturecise);

            await this.lectureciseRepository.AddAsync(newLecturecise);
            await this.lectureciseRepository.SaveChangesAsync();

            return newLecturecise.Id;
        }


        //Admin only
        public async Task<string> DeleteLectureciseById(string lectureciseId)
        {
            var lecturecise = await this.lectureciseRepository.FindbyId(lectureciseId);
            
            if (lecturecise != null && lecturecise.IsDeleted == false)
            {
                var courseId = lecturecise.CourseId;

                //this.lectureciseRepository.Delete(lecturecise);

                lecturecise.IsDeleted = true;
                lecturecise.DeletedOn = DateTime.UtcNow;

                this.lectureciseRepository.Edit(lecturecise);

                await this.lectureciseRepository.SaveChangesAsync();

                return courseId;
            }

            return null;
        }

    }
}
