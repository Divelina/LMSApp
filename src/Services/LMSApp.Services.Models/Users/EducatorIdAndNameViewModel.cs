
using AutoMapper;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Mapping;

namespace LMSApp.Services.Models.Users
{
    public class EducatorIdAndNameViewModel : IMapFrom<Educator>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Educator, EducatorIdAndNameViewModel>()
                .ForMember(x => x.FullName, m => m.MapFrom(e => $"{e.User.FirstName} {e.User.FamilyName}"));
        }
    }
}
