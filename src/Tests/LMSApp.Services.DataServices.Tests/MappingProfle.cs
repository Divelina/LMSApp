
using AutoMapper;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.Models.Courses;
using System.Linq;

namespace LMSApp.Services.DataServices.Tests
{
    public class MappingProfle : Profile
    {
        public MappingProfle()
        {
            CreateMap<LectureciseCreateBindingModel, Lecturecise>();

            CreateMap<Lecturecise, LectureciseShortViewModel>()
                .ForMember(x => x.Type, m => m.MapFrom(c => c.Type.ToString()))
                .ForMember(x => x.WeekTimes,
                            m => m.MapFrom(c => string.Join(", ",
                                              c.WeekTimes.Where(wk => wk.IsDeleted == false).OrderBy(wt => wt.DayOfWeek).Select(wt => wt.ToString()))))
                .ForMember(x => x.EducatorNames,
                            m => m.MapFrom(c => string.Join(", ", c.LectureciseEducators.Where(le => le.Educator.IsDeleted == false)
                                        .Select(le => $"{le.Educator.User.FirstName} {le.Educator.User.FamilyName}").ToList())))
                .ForMember(x => x.StudentNumber, m => m.MapFrom(src => src.LectureciseStudents.Count()))
                .ForMember(x => x.Held, m => m.MapFrom(src => $"{src.Course.Year} ({src.Course.Semester})"));

            CreateMap<Lecturecise, LectureciseDetailsViewModel>()
                .ForMember(x => x.LectureciseEducators, m => m.MapFrom(c => c.LectureciseEducators.Where(le => le.Educator.IsDeleted == false)))
                .ForMember(x => x.LectureciseStudents, m => m.MapFrom(c => c.LectureciseStudents.Where(le => le.Student.IsDeleted == false)));

            CreateMap<LectureciseDetailsViewModel, Lecturecise>()
                .ForMember(x => x.LectureciseEducators, m => m.MapFrom(c => c.LectureciseEducators))
                .ForMember(x => x.LectureciseStudents, m => m.MapFrom(c => c.LectureciseStudents));
        }
    }
}
