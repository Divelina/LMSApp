
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
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

        Student GetStudentByUserId(string userId);

        bool AnyStudent(int uniId, FacultyOf faculty);

       Task DeleteStudentByUserId(string userId);

        Task UnDeleteStudentByUserId(string userId);

        Task<IList<StudentListViewModel>> GetAllStudentsByCourse(string courseId);

        Task<IList<StudentListViewModel>> GetAllStudentsByLecturecise(string lectureciseId);
    }
}
