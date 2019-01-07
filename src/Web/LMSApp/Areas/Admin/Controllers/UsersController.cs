using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApp.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        private IUserService userService;
        private ICourseService courseService;
        private ILectureciseService lectureciseService;
        private UserManager<LMSAppUser> userManager;

        public UsersController(
            IUserService userService, 
            ICourseService courseService, 
            ILectureciseService lectureciseService,
            UserManager<LMSAppUser> userManager)
        {
            this.userService = userService;
            this.courseService = courseService;
            this.lectureciseService = lectureciseService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await this.userService.GetAll();

            users = users
                .OrderBy(u => u.IsLockedOut)
                .ThenBy(u => u.Email).ToList();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> AllStudents(StudentsFilterModel filters = null)
        {
            var students = await this.userService.GetAllStudents();

            var filteredStudents = ApplyFilters(filters, students);

            filteredStudents = filteredStudents.OrderBy(s => s.StudentUniId).ToList();

            return View(filteredStudents);
        }

        [HttpGet]
        public async Task<IActionResult> Ban(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            var lockoutEndNew = DateTime.Today.AddYears(100);

            await this.userManager.SetLockoutEndDateAsync(user, lockoutEndNew);

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> Unlock(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            var lockoutEndNew = DateTime.Today.AddYears(-1);

            await this.userManager.SetLockoutEndDateAsync(user, lockoutEndNew);

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeEducatorStatus(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (await this.userManager.IsInRoleAsync(user, "Educator"))
            {
                await this.userManager.RemoveFromRoleAsync(user, "Educator");
            }
            else
            {
                await this.userManager.AddToRoleAsync(user, "Educator");
            }

            return RedirectToAction("All");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStudentStatus(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (await this.userManager.IsInRoleAsync(user, "Student"))
            {
                await this.userManager.RemoveFromRoleAsync(user, "Student");
            }
            else
            {
                await this.userManager.AddToRoleAsync(user, "Student");
            }

            return RedirectToAction("All");
        }


        [HttpGet]
        public async Task<IActionResult> AddStudentsToCourse(StudentsFilterModel filters = null)
        {

            var students = await this.userService.GetAllStudents();

            //Applying filters

            var filteredStudents = ApplyFilters(filters, students);

            filteredStudents = filteredStudents.OrderBy(s => s.StudentUniId).ToList();

            var addToCourseModel = new StudentsAddToCourseModel()
            {
                StudentsInfo = filteredStudents
            };

            return View(addToCourseModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentsToCourse(StudentsAddToCourseModel model)
        {

            var courseId = model.CourseId;
            var lectureciceId = model.LectureciseId;
  
            if (model.StudentsInfo.Count() > 0 && this.courseService.AnyCourse(courseId))
            {
                var studentIds = model.StudentsInfo.Select(si => si.Id).ToList();
                var course = this.courseService.GetByIdOriginal(courseId);

                var addedCounter = 0;

                foreach (var studentId in studentIds)
                {
                    if (!course.StudentsInCourse.Any(st => st.StudentId == studentId))
                    {
                        course.StudentsInCourse.Add(new StudentCourse() { StudentId = studentId, CourseId = courseId });
                        addedCounter++;
                    }
                }

                await this.courseService.SaveCoursesDb();

                if (lectureciceId != null && this.lectureciseService.Any(lectureciceId))
                {
                    var lecturecise = this.lectureciseService.GetByOriginal(lectureciceId);

                    foreach (var studentId in studentIds)
                    {
                        if (!lecturecise.LectureciseStudents.Any(st => st.StudentId == studentId))
                        {
                            lecturecise.LectureciseStudents.Add(new StudentLecturecise() { StudentId = studentId, LectureciseId = lectureciceId });
                        }
                    }

                    await this.lectureciseService.SaveLecturecises();
                }

                TempData["AddedMessage"] = $"Added added {addedCounter} new students to course {course.Name}. Duplicate entries are skipped.";
            }


            return RedirectToAction("AddStudentsToCourse");
        }

        private IList<StudentListViewModel> ApplyFilters(StudentsFilterModel filters, IList<StudentListViewModel> students)
        {
            if (students.Count > 0 && !string.IsNullOrWhiteSpace(filters.Major))
            {
                students = students.Where(st => (int)st.Major == int.Parse(filters.Major)).ToList();
            }

            if (students.Count > 0 && filters.GroupNumber > 0)
            {
                students = students.Where(st => st.GroupNumber == filters.GroupNumber).ToList();
            }

            if (students.Count > 0 && !string.IsNullOrWhiteSpace(filters.FacultyName))
            {
                students = students.Where(st => (int)st.FacultyName == int.Parse(filters.FacultyName)).ToList();
            }

            if (students.Count > 0 && filters.UniIdMin > 0)
            {
                students = students.Where(st => st.StudentUniId >= filters.UniIdMin).ToList();
            }

            if (students.Count > 0 && filters.UniIdMax < int.MaxValue)
            {
                students = students.Where(st => st.StudentUniId <= filters.UniIdMax).ToList();
            }

            if (students.Count > 0 && !string.IsNullOrWhiteSpace(filters.NameStr))
            {
                students = students.Where(st => st.UserInfo.FirstName.ToLower().Contains(filters.NameStr.ToLower())
                                                || st.UserInfo.FamilyName.ToLower().Contains(filters.NameStr.ToLower())).ToList();
            }

            if (students.Count > 0 && !string.IsNullOrWhiteSpace(filters.EmailStr))
            {
                students = students.Where(st => st.UserInfo.Email.ToLower().Contains(filters.EmailStr.ToLower()))
                    .ToList();
            }

            return students;
        }
    }
}
