
using LMSApp.Services.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface IUserService
    {
        Task<IList<UserListViewModel>> GetAll();

        Task<IList<StudentListViewModel>> GetAllStudents();
    }
}
