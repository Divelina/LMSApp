
using AutoMapper;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Mapping;
using System;

namespace LMSApp.Services.Models.Assignments
{
    public class StudentAssignmentEditViewModel : IMapFrom<StudentAssignment>, IHaveCustomMappings
    {
        public string StudentId { get; set; }

        public string StudentInfoView { get; set; }

        public string AssignmentId { get; set; }

        public string LectureciseId { get; set; }

        public DateTime? DueDate { get; set; }

        public AssignmentStatus AssignmentStatus { get; set; }

        public bool Expired { get; set; }

        public string GraderId { get; set; }

        public decimal Grade { get; set; }

        public string GradeComment { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<StudentAssignment, StudentAssignmentEditViewModel>()
                .ForMember(x => x.StudentInfoView, 
                        m => m.MapFrom(src => $"{src.Student.StudentUniId}: {src.Student.User.FirstName} {src.Student.User.FamilyName}"));
        }
    }
}
