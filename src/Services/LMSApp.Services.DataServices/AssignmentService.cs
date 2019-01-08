
using AutoMapper;
using LMSApp.Data.Common;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Mapping;
using LMSApp.Services.Models.Assignments;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApp.Services.DataServices
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IRepository<Assignment> assignmentRepository;

        public AssignmentService(IRepository<Assignment> assigmnetRepository)
        {
            this.assignmentRepository = assigmnetRepository;
        }

        public async Task<string> CreateAsync(AssignmentCreateBindingModel assignment)
        {

            var newAssignment = Mapper.Map<Assignment>(assignment);

            await this.assignmentRepository.AddAsync(newAssignment);
            await this.assignmentRepository.SaveChangesAsync();

            return newAssignment.Id;
        }

        public AssignmentListViewModel GetById(string asgnId)
        {
            var assignment = this.assignmentRepository.All()
                .Where(a => a.IsDeleted == false && a.Id == asgnId)
                .To<AssignmentListViewModel>()
                .FirstOrDefault();

            return assignment;
        }

        public IList<AssignmentListViewModel> GetByCourseId(string courseId)
        {
            var assignments = this.assignmentRepository.All()
                .Where(a => a.IsDeleted == false && a.Lecturecise.CourseId == courseId)
                .To<AssignmentListViewModel>()
                .ToList();

            return assignments;
        }

        public Assignment GetByIdOriginal(string assignmentId)
        {
            var assignment = this.assignmentRepository.All()
                .Include(a => a.StudentsAssignedTo)
                .Include(a => a.Lecturecise)
                .Where(a => a.Id == assignmentId && a.IsDeleted == false)
                .FirstOrDefault();

            if (assignment != null)
            {
                return assignment;
            }

            return null;
        }

        public async Task SaveAssignmentsDb()
        {
            await this.assignmentRepository.SaveChangesAsync();
        }
    }
}
