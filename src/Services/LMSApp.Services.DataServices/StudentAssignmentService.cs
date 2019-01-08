
using LMSApp.Data.Common;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Mapping;
using LMSApp.Services.Models.Assignments;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApp.Services.DataServices
{
   public class StudentAssignmentService : IStudentAssignmentService
    {
        private readonly IRepository<StudentAssignment> studetnAssignmentRepository;

        public StudentAssignmentService(IRepository<StudentAssignment> studetnAssignmentRepository)
        {
            this.studetnAssignmentRepository = studetnAssignmentRepository;
        }

        public IList<StudentAssignmentEditViewModel> GetStudentAssignmentsById(string assignmentId)
        {
            var studentAssignments = this.studetnAssignmentRepository.All()
                .Where(sa => sa.AssignmentId == assignmentId)
                .To<StudentAssignmentEditViewModel>().ToList();

            return studentAssignments;
        }

        public IList<StudentAssignmentStudentViewModel> GetStudentAssignmentsByStudentId(string studentId)
        {
            var studentAssignments = this.studetnAssignmentRepository.All()
                .Where(sa => sa.StudentId == studentId)
                .To<StudentAssignmentStudentViewModel>().ToList();

            return studentAssignments;
        }

        public StudentAssignment GetOriginal(string assignmentId, string studentId)
        {
            var studentAssignment = this.studetnAssignmentRepository.All()
              .Where(sa => sa.AssignmentId == assignmentId && sa.StudentId == studentId)
              .FirstOrDefault();

            return studentAssignment;
        }

        public async Task SaveStudentAssignments()
        {
           await this.studetnAssignmentRepository.SaveChangesAsync();
        }
    }
}
