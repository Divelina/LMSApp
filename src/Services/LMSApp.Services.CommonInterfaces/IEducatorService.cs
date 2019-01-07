
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
        Task<string> CreateAsync(EducatorBindingModel educator);

        Educator GetByUserId(string userId);

        Task DeleteEducatorByUserId(string userId);

        Task UnDeleteEducatorByUserId(string userId);

        IEnumerable<EducatorIdAndNameViewModel> GetAll();
    }
}
