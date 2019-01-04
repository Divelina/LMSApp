
using AutoMapper;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Mapping;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LMSApp.Services.Models.Courses
{
    public class CourseListViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Semester { get; set; }
        
        public string Year { get; set; }

        public string Major { get; set; }

        public string MajorDescription { get; set; }

        public IList<LectureciseShortViewModel> Lecturecises { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Course, CourseListViewModel>()
                .ForMember(x => x.Semester, m => m.MapFrom(c => c.Semester.ToString()))
                .ForMember(x => x.Major, m => m.MapFrom(c => c.Major.ToString()))
                .ForMember(x => x.Lecturecises, m => m.MapFrom(c => c.Lecturecises.Where(le => le.IsDeleted == false)));
        }
    }
}
