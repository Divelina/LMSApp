
using AutoMapper;
using LMSApp.Data.Models;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Models.Assignments;
using LMSApp.Services.Models.Courses;
using LMSApp.Services.Models.Users;
using System.Linq;

namespace LMSApp.Services.DataServices.Tests
{
    public class MappingProfle : Profile
    {
        public MappingProfle()
        {
            CreateMap<AssignmentCreateBindingModel, Assignment>();

            CreateMap<Assignment, AssignmentListViewModel>()
                .ForMember(x => x.NumberOfStudents, opt => opt.MapFrom(src => src.StudentsAssignedTo.Count()))
                .ForMember(x => x.CourseId, opt => opt.MapFrom(src => src.Lecturecise.CourseId))
                .ForMember(x => x.CourseHeld, opt =>
                        opt.MapFrom(src => $"{src.Lecturecise.Course.Name} ({src.Lecturecise.Course.Year} {src.Lecturecise.Course.Semester})"));

            CreateMap<CourseCreateBindingModel, Course>();

            CreateMap<Course, CourseListViewModel>()
                .ForMember(x => x.Semester, m => m.MapFrom(c => c.Semester.ToString()))
                .ForMember(x => x.Major, m => m.MapFrom(c => c.Major.ToString()))
                .ForMember(x => x.Lecturecises, m => m.MapFrom(c => c.Lecturecises.Where(le => le.IsDeleted == false)));

            CreateMap<Course, CourseDetailsViewModel>()
               .ForMember(x => x.Lecturecises, m => m.MapFrom(c => c.Lecturecises.Where(l => l.IsDeleted == false)))
               .ForMember(x => x.StudentsInCourse, m => m.MapFrom(c => c.StudentsInCourse.Where(st => st.Student.IsDeleted == false)));

            CreateMap<CourseDetailsViewModel, Course>()
                .ForMember(x => x.Lecturecises, m => m.MapFrom(c => c.Lecturecises))
                .ForMember(x => x.StudentsInCourse, m => m.MapFrom(c => c.StudentsInCourse));

            CreateMap<EducatorBindingModel, Educator>();

            CreateMap<Educator, EducatorIdAndNameViewModel>()
                .ForMember(x => x.FullName, m => m.MapFrom(e => $"{e.User.FirstName} {e.User.FamilyName}"));

            CreateMap<GroupViewModel, Group>();

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

            CreateMap<StudentAssignment, StudentAssignmentEditViewModel>()
                .ForMember(x => x.StudentInfoView,
                        m => m.MapFrom(src => $"{src.Student.StudentUniId}: {src.Student.User.FirstName} {src.Student.User.FamilyName}"));

            CreateMap<StudentAssignment, StudentAssignmentStudentViewModel>()
                .ForMember(x => x.AssignmentName, m => m.MapFrom(src => src.Assignment.Name))
                .ForMember(x => x.DateCreated, m => m.MapFrom(src => src.Assignment.DateCreated))
                .ForMember(x => x.MaxGrade, m => m.MapFrom(src => src.Assignment.MaxGrade))
                .ForMember(x => x.CourseInfo, m => m.MapFrom(src => $"{src.Lecturecise.Course.Name} ({src.Lecturecise.Course.Year})"))
                .ForMember(x => x.PercentOfClass, m => m.MapFrom(src =>
                   (100m * src.Assignment.StudentsAssignedTo.Where(sa => sa.Grade < src.Grade).Count()) /
                   src.Assignment.StudentsAssignedTo.Where(sa => sa.Grade > 0).Count()
                   ));

            CreateMap<StudentBindingModel, Student>();

            CreateMap<Student, StudentListViewModel>();

            CreateMap<LMSAppUser, UserListViewModel>();
        }
    }
}
