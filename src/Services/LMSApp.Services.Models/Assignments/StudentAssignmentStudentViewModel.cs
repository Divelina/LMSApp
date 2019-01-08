
using AutoMapper;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Mapping;
using System;
using System.Linq;

namespace LMSApp.Services.Models.Assignments
{
    public class StudentAssignmentStudentViewModel : IMapFrom<StudentAssignment>, IHaveCustomMappings
    {
        public string StudentId { get; set; }

        public string AssignmentId { get; set; }

        public string AssignmentName { get; set; }
        
        public DateTime? DateCreated { get; set; }

        public string CourseInfo { get; set; }

        public string LectureciseId { get; set; }

        public DateTime? DueDate { get; set; }

        public AssignmentStatus AssignmentStatus { get; set; }

        public bool Expired { get; set; }

        public decimal MaxGrade { get; set; }

        public decimal Grade { get; set; }

        public string GradeComment { get; set; }

        public decimal PercentOfClass { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<StudentAssignment, StudentAssignmentStudentViewModel>()
                .ForMember(x => x.AssignmentName, m => m.MapFrom(src => src.Assignment.Name))
                .ForMember(x => x.DateCreated, m => m.MapFrom(src => src.Assignment.DateCreated))
                .ForMember(x => x.MaxGrade, m => m.MapFrom(src => src.Assignment.MaxGrade))
                .ForMember(x => x.CourseInfo, m => m.MapFrom(src => $"{src.Lecturecise.Course.Name} ({src.Lecturecise.Course.Year})"))
                .ForMember(x => x.PercentOfClass, m => m.MapFrom(src =>
                   (100m*src.Assignment.StudentsAssignedTo.Where(sa => sa.Grade < src.Grade).Count())/
                   src.Assignment.StudentsAssignedTo.Where(sa => sa.Grade > 0).Count()
                   ));
        }
    }
}
