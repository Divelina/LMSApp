
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
    }
}
