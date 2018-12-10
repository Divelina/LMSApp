
using LMSApp.Services.Models.Courses;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface ICourseService
    {
        Task<string> CreateAsync(CourseCreateBindingModel course);
    }
}
