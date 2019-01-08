
using AutoMapper;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LMSApp.Services.Models.Assignments
{
    public class AssignmentListViewModel : IMapFrom<Assignment>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime? DateCreated { get; set; }

        public AssignmentType AssignmentType { get; set; }

        public decimal MaxGrade { get; set; }

        [Required]
        public string LectureciseId { get; set; }

        [Required]
        public string EducatorId { get; set; }

        [Required]
        public string CourseId { get; set; }

        public string CourseHeld { get; set; }

        public int NumberOfStudents { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Assignment, AssignmentListViewModel>()
                .ForMember(x => x.NumberOfStudents, opt => opt.MapFrom(src => src.StudentsAssignedTo.Count()))
                .ForMember(x => x.CourseId, opt => opt.MapFrom(src => src.Lecturecise.CourseId))
                .ForMember(x => x.CourseHeld, opt => 
                        opt.MapFrom(src => $"{src.Lecturecise.Course.Name} ({src.Lecturecise.Course.Year} {src.Lecturecise.Course.Semester})"));
        }
    }
}
