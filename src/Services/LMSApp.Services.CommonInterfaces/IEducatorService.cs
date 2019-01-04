
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Models.Courses;
using LMSApp.Services.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface IEducatorService
    {
        Educator GetByUserId(string userId);

        IEnumerable<EducatorIdAndNameViewModel> GetAll();
    }
}
