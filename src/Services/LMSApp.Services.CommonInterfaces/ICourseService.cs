
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Models.Courses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface ICourseService
    {
        Task<string> CreateAsync(CourseCreateBindingModel course);

        bool AnyCourse(string name, Semester semester, string year, Major major);

        IEnumerable<CourseListViewModel> GetAll();

        Course GetByIdOriginal(string courseId);

        Task<CourseDetailsViewModel> GetCourseById(string courseId);

        Task EditCourseById(CourseDetailsViewModel courseModel);

        Task DeleteCourseById(string courseId);

        IEnumerable<CourseListViewModel> GetAllByEducator(string educatorId);
    }
}
