using AutoMapper;

using System.Threading.Tasks;
using LMSApp.Data.Common;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Courses;
using LMSApp.Data.Models.Enums;
using System.Linq;
using System.Collections.Generic;
using LMSApp.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using System;

namespace LMSApp.Services.DataServices
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> coursesRepository;

        public CourseService(IRepository<Course> coursesRepository)
        {
            this.coursesRepository = coursesRepository;
        }

        public async Task<string> CreateAsync(CourseCreateBindingModel course)
        {

            var newCourse = Mapper.Map<Course>(course);

            await this.coursesRepository.AddAsync(newCourse);
            await this.coursesRepository.SaveChangesAsync();

            return newCourse.Id;
        }

        public bool AnyCourse(string name, Semester semester, string year, Major major)
        {
            var isCourseFound = this.coursesRepository.All().Any(c =>
                 c.Name == name &&
                 c.Semester == semester &&
                 c.Year == year &&
                 c.Major == major &&
                 c.IsDeleted == false);

            return isCourseFound;
        }

        public bool AnyCourse(string id)
        {
            var isCourseFound = this.coursesRepository.All().Any(c =>
                 c.Id == id && c.IsDeleted == false);

            return isCourseFound;
        }

        public IEnumerable<CourseListViewModel> GetAll()
        {
            var courses = this.coursesRepository.All()
                .Where(c => c.IsDeleted == false)
                .To<CourseListViewModel>();

            return courses;
        }

        //public async Task<CourseDetailsViewModel> GetCourseById(string courseId)
        //{
        //    var course = await this.coursesRepository.FindbyId(courseId);

        //    var courseModel = Mapper.Map<CourseDetailsViewModel>(course);

        //    return courseModel;
        //}

        public async Task<CourseDetailsViewModel> GetCourseById(string courseId)
        {
            var course = await this.coursesRepository.FindbyId(courseId);

            if (course != null && course.IsDeleted == false)
            {
                var courseModel = this.coursesRepository.All()
                    .Where(c => c.Id == courseId)
                    .To<CourseDetailsViewModel>()
                    .FirstOrDefault();

                return courseModel;
            }

            return null;
        }

        public async Task EditCourseById(CourseDetailsViewModel courseModel)
        {
            var isCourseFound = this.AnyCourse(courseModel.Id);

            if (isCourseFound)
            {
                var newCourse = Mapper.Map<Course>(courseModel);

                this.coursesRepository.Edit(newCourse);

                await this.coursesRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteCourseById(string courseId)
        {
            var course = await this.coursesRepository.FindbyId(courseId);

            if (course != null && course.IsDeleted == false)
            {
                //this.coursesRepository.Delete(course);

                course.IsDeleted = true;
                course.DeletedOn = DateTime.UtcNow;

                this.coursesRepository.Edit(course);

                await this.coursesRepository.SaveChangesAsync();
            }
        }

        //Educator only services 

        public IEnumerable<CourseListViewModel> GetAllByEducator(string educatorId)
        {
            var courses = this.coursesRepository.All()
                .Where(c => c.CourseEducators.Any(ce => ce.EducatorId == educatorId
                        && c.IsDeleted == false))
                .To<CourseListViewModel>();

            return courses;
        }
    }
}
