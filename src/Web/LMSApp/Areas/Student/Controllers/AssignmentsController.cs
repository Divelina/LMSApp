
using LMSApp.Data.Models;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Assignments;
using LMSApp.Services.Models.Chart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApp.Areas.Student.Controllers
{

    public class AssignmentsController : StudentBaseController
    {
        private IAssignmentService assignmentService;
        private ICourseService courseService;
        private IEducatorService educatorService;
        private IStudentAssignmentService studentAssignmentService;
        private IUserService userService;
        private UserManager<LMSAppUser> userManager;

        public AssignmentsController(
            IAssignmentService assignmentService,
            ICourseService courseService,
            IEducatorService educatorService,
            IStudentAssignmentService studentAssignmentService,
            IUserService userService,
            UserManager<LMSAppUser> userManager)
        {
            this.assignmentService = assignmentService;
            this.courseService = courseService;
            this.educatorService = educatorService;
            this.studentAssignmentService = studentAssignmentService;
            this.userService = userService;
            this.userManager = userManager;
        }

        //All assignments of the current student.
        [HttpGet]
        public async Task<IActionResult> All(AssignmentFilterModel filters = null)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userId = user.Id;

            var studentId = this.userService.GetStudentByUserId(userId).Id;

            var assignments = this.studentAssignmentService.GetStudentAssignmentsByStudentId(studentId);

            assignments = assignments.OrderByDescending(a => a.DateCreated).ToList();
            
            if (assignments == null)
            {
                return View(new List<StudentAssignmentStudentViewModel>());
            }

            return View(assignments);
        }

    }
}
