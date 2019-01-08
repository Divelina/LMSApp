
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Services.Models.Assignments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.CommonInterfaces
{
   public interface IAssignmentService
    {
        Task<string> CreateAsync(AssignmentCreateBindingModel assignment);

        AssignmentListViewModel GetById(string courseId);
        
        Assignment GetByIdOriginal(string assignmentId);

        IList<AssignmentListViewModel> GetByCourseId(string courseId);

        Task SaveAssignmentsDb();
    }
}
