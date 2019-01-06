
using LMSApp.Services.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface IUserService
    {
        Task<IList<UserListViewModel>> GetAll();

        Task<IList<StudentListViewModel>> GetAllStudents();

        Task<IList<StudentListViewModel>> GetAllStudentsByCourse(string courseId);

        Task<IList<StudentListViewModel>> GetAllStudentsByLecturecise(string lectureciseId);
    }
}
