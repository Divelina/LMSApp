
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
    public class CourseDetailsViewModel : IMapFrom<Course>, IMapTo<Course>, IHaveCustomMappings
    {

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Semester Semester { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{4}\/[0-9]{4}", ErrorMessage = "Year should be in the format 2018/2019!")]
        public string Year { get; set; }

        [Required]
        public Major Major { get; set; }

        public string MajorDescription { get; set; }

        public string Description { get; set; }

        public IList<LectureciseShortViewModel> Lecturecises { get; set; }

        public IList<StudentCourse> StudentsInCourse { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Course, CourseDetailsViewModel>()
                .ForMember(x => x.Lecturecises, m => m.MapFrom(c => c.Lecturecises.Where(l => l.IsDeleted == false)))
                .ForMember(x => x.StudentsInCourse, m => m.MapFrom(c => c.StudentsInCourse.Where(st => st.Student.IsDeleted == false)));

            configuration.CreateMap<CourseDetailsViewModel, Course>()
                .ForMember(x => x.Lecturecises, m => m.MapFrom(c => c.Lecturecises))
                .ForMember(x => x.StudentsInCourse, m => m.MapFrom(c => c.StudentsInCourse));
        }
    }

}