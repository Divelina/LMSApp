
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

namespace LMSApp.Areas.Educator.Controllers
{

    public class AssignmentsController : EducatorBaseController
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

        private static int BinNumberDefault = 10;

        [HttpGet]
        [Authorize(Roles = "Educator")]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Educator")]
        public async Task<IActionResult> Create(AssignmentCreateBindingModel assignment)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(assignment);
            }

            if (assignment.DateCreated == null)
            {
                assignment.DateCreated = DateTime.Now;
            }

            if (assignment.DueDate != null && assignment.DueDate < assignment.DateCreated)
            {

                TempData["Error"] +=
                    "Error: The due date should be after the assignment create date.";

                return this.View(assignment);
            }

            var assignmentId = await this.assignmentService.CreateAsync(assignment);

            var studentIds = assignment.StudentIds;

            if (studentIds.Contains("all"))
            {
                var students = await this.userService.GetAllStudentsByLecturecise(assignment.LectureciseId);

                studentIds = students.Select(st => st.Id).ToList();
            }

            if (studentIds.Count() > 0)
            {
                var createdAssignment = this.assignmentService.GetByIdOriginal(assignmentId);

                foreach (var studentId in studentIds)
                {
                    if (!createdAssignment.StudentsAssignedTo.Any(s => s.StudentId == studentId))
                    {
                        var studentAssignment = new StudentAssignment()
                        {
                            AssignmentId = createdAssignment.LectureciseId,
                            StudentId = studentId,
                            LectureciseId = createdAssignment.LectureciseId,
                            DueDate = assignment.DueDate,
                            AssignmentStatus = AssignmentStatus.Unfinished,
                            Expired = false,
                            Grade = 0m
                        };

                        createdAssignment.StudentsAssignedTo.Add(studentAssignment);
                    }
                }

                await this.assignmentService.SaveAssignmentsDb();
            }


            //TODO - change redirect to all assignments View when I make it
            return RedirectToAction("CurrentYearLecturecises", "Lecturecises", new { Area = "Educator" });
        }

        //All assignments for the courses of the current educator.
        [HttpGet]
        [Authorize(Roles = "Educator")]
        public async Task<IActionResult> AssignmentsInCourses(AssignmentFilterModel filters = null)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var educatorId = this.educatorService.GetByUserId(user.Id).Id;

            //Should not be possible but ...
            if(educatorId == null)
            {
                return View("../AccessDenied");
            }

            var educatorCoursesIds = this.courseService.GetAllByEducator(educatorId).Select(c => c.Id);

            var assignments = new List<AssignmentListViewModel>();

            foreach (var courseId in educatorCoursesIds)
            {
                var courseAssignments = this.assignmentService.GetByCourseId(courseId);

                if (courseAssignments != null && courseAssignments.Count() > 0)
                {
                    assignments.AddRange(courseAssignments);
                }
            }

            var filteredAssignments = ApplyAssignmentFilters(filters, assignments);

            filteredAssignments = filteredAssignments.OrderByDescending(a => a.DateCreated).ToList();

            return View(filteredAssignments);
        }

        [HttpGet]
        [Authorize(Roles = "Educator")]
        public async Task<IActionResult> StudentAssignmentsManage(string asgnId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var educatorId = this.educatorService.GetByUserId(user.Id).Id;

            //Should not be possible but ...
            if (educatorId == null)
            {
                return View("../AccessDenied");
            }

            var educatorCoursesIds = this.courseService.GetAllByEducator(educatorId).Select(c => c.Id);

            var assignment = this.assignmentService.GetById(asgnId);

            //If the educator is not involved in the current course.
            if (!educatorCoursesIds.Contains(assignment.CourseId))
            {
                return View("../AccessDenied");
            }
            
            //In order to show some assignment info in the view (not to be editted).
            ViewData["assignmentName"] = assignment.Name;
            ViewData["assignmentCourseHeld"] = assignment.CourseHeld;
            ViewData["assignmentDateCreated"] = assignment.DateCreated;
            ViewData["assignmentMaxGrade"] = assignment.MaxGrade.ToString();

            //var studentAssignments = this.assignmentService.GetStudentAssignmentsById(asgnId);

            var studentAssignments = this.studentAssignmentService.GetStudentAssignmentsById(asgnId);

            return View(studentAssignments);
        }

        [HttpPost]
        [Authorize(Roles = "Educator")]
        public async Task<IActionResult> StudentAssignmentsManage(IList<StudentAssignmentEditViewModel> studentAssignments, string asgnId)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var educatorId = this.educatorService.GetByUserId(user.Id).Id;

            if (studentAssignments.Count() > 0)
            {
                foreach (var studentAssignment in studentAssignments)
                {
                    var assignment = this.studentAssignmentService.GetOriginal(asgnId, studentAssignment.StudentId);

                    assignment.AssignmentStatus = studentAssignment.AssignmentStatus;
                    assignment.DueDate = studentAssignment.DueDate;
                    assignment.GradeComment = studentAssignment.GradeComment;
                    
                    if (assignment.Grade != studentAssignment.Grade)
                    {
                        assignment.Grade = studentAssignment.Grade;
                        assignment.GraderId = educatorId;
                    }
                }

                await this.studentAssignmentService.SaveStudentAssignments();
            }

            return RedirectToAction("StudentAssignmentsManage", new { asgnId = asgnId });
        }

        //Statistics are viewable by any Educator or Admin who knows the assignment Id
        [HttpGet]
        public IActionResult ViewStats(string asgnId, string binNumberStr = null)
        {
            var binNumber = BinNumberDefault;
            if(binNumberStr != null)
            {
                var isParsed = int.TryParse(binNumberStr, out binNumber);

                if (!isParsed)
                {
                    binNumber = BinNumberDefault;
                    TempData["Error"] = "Default bin value is {BinNumberDefault}. Please enter an integer value for the bins!";
                }
            }
            
            var assignment = this.assignmentService.GetById(asgnId);

            var studentAssignments = this.studentAssignmentService.GetStudentAssignmentsById(asgnId);

            var nonZerogrades = studentAssignments.Where(a => a.Grade != 0m).Select(a => a.Grade).ToList();

            var maxGrade = assignment.MaxGrade;

            ViewData["assignmentId"] = asgnId;
            ViewData["assignmentName"] = assignment.Name;
            ViewData["assignmentCourseHeld"] = assignment.CourseHeld;
            ViewData["assignmentDateCreated"] = assignment.DateCreated;
            ViewData["studentsWithAssignment"] = studentAssignments.Count().ToString();
            ViewData["assignmentMaxGrade"] = maxGrade.ToString();
            ViewData["assignmentAverageGrade"] = nonZerogrades.Average().ToString("0.0");
            ViewData["assignmentStDevGrade"] = StandardDeviation(nonZerogrades).ToString("0.0");
            ViewData["gradedStudents"] = nonZerogrades.Count().ToString();
            ViewData["binNumber"] = binNumber.ToString();

            var lstModel = PrepareHistogram(nonZerogrades, maxGrade, binNumber);

            return View(lstModel);
        }

        private double StandardDeviation(List<decimal> nonZerogrades)
        {
            var squaredDiffernceSum = 0.0;
            var average = nonZerogrades.Average();

            foreach (var grade in nonZerogrades)
            {
                squaredDiffernceSum += Math.Pow((double)average - (double)grade, 2);
            }

            squaredDiffernceSum = squaredDiffernceSum / (nonZerogrades.Count() - 1);

            return Math.Sqrt(squaredDiffernceSum);
        }

        private List<SimpleReportViewModel> PrepareHistogram(List<decimal> nonZerogrades, decimal maxGrade, int binNumber)
        {
            var binValues = new List<int>();

            var gradeStep = maxGrade / binNumber;

            var percentStep = 100m / binNumber;

            for (int i = 0; i < binNumber; i++)
            {
                binValues.Add(0);
            }

            for (int i = 0; i < nonZerogrades.Count(); i++)
            {
                var binIndex = (int)Math.Ceiling(nonZerogrades[i] / gradeStep) - 1;
                binValues[binIndex]++;
            }

            var histogramModel = new List<SimpleReportViewModel>();
            var percentage = 0m;

            for (int i = 0; i < binNumber; i++)
            {

                histogramModel.Add(new SimpleReportViewModel()
                {
                    DimensionOne = $"{percentage.ToString("0")} - {(percentage + percentStep).ToString("0")} % ",
                    Quantity = binValues[i]
                });

                percentage += percentStep;
            }

            return histogramModel;
        }

        private IList<AssignmentListViewModel> ApplyAssignmentFilters(AssignmentFilterModel filters, List<AssignmentListViewModel> assignments)
        {
            return assignments;
        }
    }
}
