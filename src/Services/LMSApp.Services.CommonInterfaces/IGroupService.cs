
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface IGroupService
    {
        bool AnyGroup(int number, Major major);

        IEnumerable<GroupViewModel> GetAll();

        Task<string> CreateAsync(GroupViewModel group);
    }
}
