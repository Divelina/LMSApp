using AutoMapper;

using System.Threading.Tasks;
using LMSApp.Data.Common;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Courses;
using LMSApp.Data.Models.Enums;
using System.Linq;

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
                 c.Major == major);

            return isCourseFound;
        }
    }
}
