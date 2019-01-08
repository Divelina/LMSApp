
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Services.Models.Assignments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
    public interface IStudentAssignmentService
    {
        IList<StudentAssignmentEditViewModel> GetStudentAssignmentsById(string assignmentId);

        IList<StudentAssignmentStudentViewModel> GetStudentAssignmentsByStudentId(string studentId);

        StudentAssignment GetOriginal(string assignmentId, string studentId);

        Task SaveStudentAssignments();
    }
}
