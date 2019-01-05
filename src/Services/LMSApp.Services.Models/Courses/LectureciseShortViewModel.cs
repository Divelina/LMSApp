
using AutoMapper;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.Mapping;
using LMSApp.Services.Models.Users;
using System.Collections.Generic;
using System.Linq;

namespace LMSApp.Services.Models.Courses
{
    public class LectureciseShortViewModel : IMapFrom<Lecturecise>, IMapTo<Lecturecise>, IHaveCustomMappings
    {
     
        public string Id { get; set; }

        public string CourseId { get; set; }

        public string Type { get; set; }

        public string WeekTimes { get; set; }

        public string EducatorNames { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Lecturecise, LectureciseShortViewModel>()
                .ForMember(x => x.Type, m => m.MapFrom(c => c.Type.ToString()))
                .ForMember(x => x.WeekTimes,
                            m => m.MapFrom(c => string.Join(", ",
                                              c.WeekTimes.Where(wk => wk.IsDeleted == false).OrderBy(wt => wt.DayOfWeek).Select(wt => wt.ToString()))))
                .ForMember(x => x.EducatorNames,
                            m => m.MapFrom(c => string.Join(", ", c.LectureciseEducators.Where(le => le.Educator.IsDeleted == false)
                                        .Select(le => $"{le.Educator.User.FirstName} {le.Educator.User.FamilyName}").ToList())));
        }
    }
}
