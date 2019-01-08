
using AutoMapper;
using LMSApp.Data.Common;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Mapping;
using LMSApp.Services.Models.Users;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LMSApp.Services.DataServices
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Data.Models.UserTypes.Group> groupRepository;

        public GroupService(IRepository<Data.Models.UserTypes.Group> groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public bool AnyGroup(int number, Major major)
        {
            var isFound = this.groupRepository.All().Any(g =>
                 g.Number == number &&
                 g.Major == major &&
                 g.IsDeleted == false);

            return isFound;
        }

        public async Task<string> CreateAsync(GroupViewModel course)
        {

            var newGroup = Mapper.Map<Data.Models.UserTypes.Group>(course);

            await this.groupRepository.AddAsync(newGroup);
            await this.groupRepository.SaveChangesAsync();

            return newGroup.Id;
        }

        public IEnumerable<GroupViewModel> GetAll()
        {
            var groups = this.groupRepository.All()
                .Where(c => c.IsDeleted == false)
                .To<GroupViewModel>();

            return groups;
        }
    }
}
