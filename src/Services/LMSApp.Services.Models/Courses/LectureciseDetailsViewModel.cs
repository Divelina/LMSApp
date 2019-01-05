
using AutoMapper;
using LMSApp.Data.Models;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Mapping;
using LMSApp.Services.Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LMSApp.Services.Models.Courses
{
    public class LectureciseDetailsViewModel : IMapFrom<Lecturecise>, IMapTo<Lecturecise>, IHaveCustomMappings
    {

        public string Id { get; set; }

        [Required]
        public LectureciseType Type { get; set; }

        [Required]
        public string CourseId { get; set; }

        public IList<WeekTime> WeekTimes { get; set; }

        public IList<EducatorLecturecise> LectureciseEducators { get; set; }

        public IList<StudentLecturecise> LectureciseStudents { get; set; }

        //public virtual IList<Event> Events { get; set; }

        //public virtual IList<StudentAssignment> StudentTasks { get; set; }


        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Lecturecise, LectureciseDetailsViewModel>()
                .ForMember(x => x.LectureciseEducators, m => m.MapFrom(c => c.LectureciseEducators.Where(le=> le.Educator.IsDeleted == false)))
                .ForMember(x => x.LectureciseStudents, m => m.MapFrom(c => c.LectureciseStudents.Where(le => le.Student.IsDeleted == false)));

            configuration.CreateMap<LectureciseDetailsViewModel, Lecturecise>()
                .ForMember(x => x.LectureciseEducators, m => m.MapFrom(c => c.LectureciseEducators))
                .ForMember(x => x.LectureciseStudents, m => m.MapFrom(c => c.LectureciseStudents));
        }
    }
}
