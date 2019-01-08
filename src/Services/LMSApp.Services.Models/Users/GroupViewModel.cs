
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Mapping;

namespace LMSApp.Services.Models.Users
{
    public class GroupViewModel : IMapFrom<Group>, IMapTo<Group>
    {
        public string Id { get; set; }

        public int Number { get; set; }

        public string Description { get; set; }

        public Major Major { get; set; }
    }
}
