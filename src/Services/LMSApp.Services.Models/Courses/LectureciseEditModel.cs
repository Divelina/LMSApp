
using AutoMapper;
using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Mapping;
using LMSApp.Services.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LMSApp.Services.Models.Courses
{
    public class LectureciseEditModel : IMapFrom<Lecturecise>, IMapTo<Lecturecise>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public LectureciseType Type { get; set; }

        public string CourseId { get; set; }

        public IEnumerable<string> EducatorIdsForForm { get; set; }

        public DayOfWeek? Day1 { get; set; }

        public DateTime? StartTime1 { get; set; }

        public DateTime? EndTime1 { get; set; }

        public DayOfWeek? Day2 { get; set; }

        public DateTime? StartTime2 { get; set; }

        public DateTime? EndTime2 { get; set; }

        public string EducatorNames { get; set; }

        //public IList<EducatorIdAndNameViewModel> Educators { get; set; }

        public IList<WeekTime> WeekTimes { get; set; }

        public IList<EducatorLecturecise> LectureciseEducators { get; set; }

        public IList<StudentLecturecise> LectureciseStudents { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Lecturecise, LectureciseEditModel>()
                .ForMember(x => x.LectureciseEducators, m => m.MapFrom(c => c.LectureciseEducators.Where(le => le.Educator.IsDeleted == false)))
                .ForMember(x => x.WeekTimes, m => m.MapFrom(c => c.WeekTimes.Where(w => w.IsDeleted == false)))
                //.ForMember(x => x.Educators, m => m.MapFrom(c => c.LectureciseEducators.Select(le =>  new EducatorIdAndNameViewModel()
                //{
                //    Id = le.EducatorId,
                //    FullName = $"{le.Educator.User.FirstName} {le.Educator.User.FamilyName}"
                //})))
                .ForMember(x => x.EducatorNames,
                            m => m.MapFrom(c => string.Join(", ", c.LectureciseEducators.Where(le => le.Educator.IsDeleted == false)
                                        .Select(le => $"{le.Educator.User.FirstName} {le.Educator.User.FamilyName}").ToList())))
                .ForMember(x => x.LectureciseStudents, m => m.MapFrom(c => c.LectureciseStudents.Where(le => le.Student.IsDeleted == false)));

            configuration.CreateMap<LectureciseEditModel, Lecturecise>()
                .ForMember(x => x.LectureciseEducators, m => m.MapFrom(c => c.LectureciseEducators))
                .ForMember(x => x.LectureciseStudents, m => m.MapFrom(c => c.LectureciseStudents));
        }
    }
}
