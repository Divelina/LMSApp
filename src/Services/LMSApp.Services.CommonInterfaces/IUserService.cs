
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface IUserService
    {
        Task<IList<UserListViewModel>> GetAll();

        Task<string> CreateAsync(StudentBindingModel student);

        Task<IList<StudentListViewModel>> GetAllStudents();

        bool AnyStudent(int uniId, FacultyOf faculty);

        Task<IList<StudentListViewModel>> GetAllStudentsByCourse(string courseId);

        Task<IList<StudentListViewModel>> GetAllStudentsByLecturecise(string lectureciseId);
    }
}
