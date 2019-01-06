using AutoMapper;
using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Courses;
using LMSApp.Services.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApp.Areas.Educator.Controllers
{
    public class LecturecisesController : EducatorBaseController
    {
        private readonly ILectureciseService lectureciseService;
        private readonly ICourseService courseService;
        private readonly IEducatorService educatorService;
        private readonly IEducatorLectureciseService educatorLectureciseService;
        private readonly IWeekTimeService weekTimeService;
        private readonly IUserService userService;
        private UserManager<LMSAppUser> userManager;

        public LecturecisesController(
            ILectureciseService lectureciseService,
            IEducatorService educatorService,
            ICourseService courseService,
            IEducatorLectureciseService educatorLectureciseService,
            IWeekTimeService weekTimeService,
            IUserService userService,
            UserManager<LMSAppUser> userManager)
        {
            this.lectureciseService = lectureciseService;
            this.courseService = courseService;
            this.educatorService = educatorService;
            this.educatorLectureciseService = educatorLectureciseService;
            this.weekTimeService = weekTimeService;
            this.userService = userService;
            this.userManager = userManager;
        }

        //For Educator's only own courses in the current year
        [HttpGet]
        public async Task<IActionResult> AllLecturecises()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var lecturecises = this.lectureciseService.GetAll()
                .Where(l => l.EducatorNames.ToLower().Contains(user.FamilyName.ToLower()) &&
                    l.EducatorNames.ToLower().Contains(user.FirstName.ToLower()))
                .ToList();

            return View(lecturecises);
        }

        //For Educator's own courses in the current year
        [HttpGet]
        public async Task<IActionResult> CurrentYearLecturecises()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var coursesIdThisYear = this.courseService.GetAll()
                .Where(c => c.Year.Contains(DateTime.Now.Year.ToString()))
                .Select(c => c.Id)
                .ToList();

            var lecturecises = this.lectureciseService.GetAll()
                .Where(l => l.EducatorNames.ToLower().Contains(user.FamilyName.ToLower()) &&
                    l.EducatorNames.ToLower().Contains(user.FirstName.ToLower()) &&
                    coursesIdThisYear.Any(ci => ci == l.CourseId))
                .ToList();

            return View(lecturecises);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string lectureciseId)
        {
            //TODO return Error badrequest;
            //if lecturecise == null - though it's not likely

            var user = await this.userManager.GetUserAsync(this.User);

            var lecturecise = this.lectureciseService.GetByOriginal(lectureciseId);

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                var educator = this.educatorService.GetByUserId(user.Id);

                if (!lecturecise.LectureciseEducators.Any(e => e.EducatorId == educator.Id))
                {
                    return View("../AccessDenied");
                };

            }

            var lectureciseModel = Mapper.Map<LectureciseEditModel>(lecturecise);

            TempData["lectureciseId"] = lectureciseId;
            TempData["courseId"] = lecturecise.CourseId;

            return View(lectureciseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LectureciseEditModel lectureciseModel)
        {
            var lecturecise = this.lectureciseService.GetByOriginal(lectureciseModel.Id);

            if (lectureciseModel.Type != null && lectureciseModel.Type != lecturecise.Type)
            {
                lecturecise.Type = lectureciseModel.Type;
            }

            if (lectureciseModel.EducatorIdsForForm != null || lectureciseModel.Day1 != null || lectureciseModel.Day2 != null)
            {

                if (lectureciseModel.EducatorIdsForForm != null)
                {
                    var course = this.courseService.GetByIdOriginal(lectureciseModel.CourseId);

                    foreach (var educatorId in lectureciseModel.EducatorIdsForForm)
                    {
                        lecturecise.LectureciseEducators.Add(new EducatorLecturecise() { EducatorId = educatorId, LectureciseId = lecturecise.Id });

                        //Add the Educators to the course if not added previously.
                        if (course != null && !course.CourseEducators.Any(ce => ce.EducatorId == educatorId))
                        {
                            course.CourseEducators.Add(new EducatorCourse()
                            {
                                EducatorId = educatorId,
                                CourseId = course.Id,
                                TeachingRole = lectureciseModel.Type == LectureciseType.Lecture ? TeachingRole.Lecturer : TeachingRole.Assistant
                            });
                        }
                    }
                }

                if (lectureciseModel.Day1 != null && lectureciseModel.StartTime1 != null)
                {
                    var weekTime1 = new WeekTime()
                    {
                        DayOfWeek = lectureciseModel.Day1.Value,
                        StartHour = lectureciseModel.StartTime1.Value.TimeOfDay.ToString()
                    };

                    if (lectureciseModel.EndTime1 != null)
                    {
                        weekTime1.EndHour = lectureciseModel.EndTime1.Value.TimeOfDay.ToString();
                    }

                    lecturecise.WeekTimes.Add(weekTime1);
                }

                if (lectureciseModel.Day2 != null && lectureciseModel.StartTime2 != null)
                {
                    var weekTime2 = new WeekTime()
                    {
                        DayOfWeek = lectureciseModel.Day2.Value,
                        StartHour = lectureciseModel.StartTime2.Value.TimeOfDay.ToString()
                    };

                    if (lectureciseModel.EndTime2 != null)
                    {
                        weekTime2.EndHour = lectureciseModel.EndTime1.Value.TimeOfDay.ToString();
                    }

                    lecturecise.WeekTimes.Add(weekTime2);
                }

                await this.lectureciseService.SaveLecturecises();
                await this.courseService.SaveCoursesDb();

            }

            return RedirectToAction("AllLecturecises");
        }

        [HttpGet]
        public IActionResult ClearWeektimes(string lectureciseId)
        {
            var lecturecise = this.lectureciseService.GetByOriginal(lectureciseId);

            var weekTimes = lecturecise.WeekTimes;

            foreach (var weekTime in weekTimes)
            {
                this.weekTimeService.DeleteById(weekTime.Id);
            }

            this.lectureciseService.SaveLecturecises();

            return RedirectToAction("Edit", new { lectureciseId = lectureciseId });
        }

        [HttpGet]
        public IActionResult ClearEducators(string lectureciseId)
        {
            var lecturecise = this.lectureciseService.GetByOriginal(lectureciseId);

            this.educatorLectureciseService.DeleteById(lectureciseId);

            this.lectureciseService.SaveLecturecises();

            //TODO - Clear educators from course if in none of the lecturecises
            return RedirectToAction("Edit", new { lectureciseId = lectureciseId });
        }

        [HttpGet]
        public async Task<IActionResult> AddStudentsToLecturecise(StudentsFilterModel filters, string lectureciseId)
        {
            if(lectureciseId == null)
            {
                lectureciseId = filters.ParamId;
            }

            var lecturecise = await this.lectureciseService.GetById(lectureciseId);

            var students = await this.userService.GetAllStudentsByCourse(lecturecise.CourseId);

            var alreadyAddedStudents = await this.userService.GetAllStudentsByLecturecise(lecturecise.CourseId);

            students = students.Where(st => !alreadyAddedStudents.Any(aas => aas.Id == st.Id)).ToList();

            //Applying filters

            var filteredStudents = ApplyFilters(filters, students);

            filteredStudents = filteredStudents.OrderBy(s => s.StudentUniId).ToList();

            var addToCourseModel = new StudentsAddToCourseModel()
            {
                CourseId = lecturecise.CourseId,
                LectureciseId = lecturecise.Id,
                StudentsInfo = filteredStudents
            };

            return View(addToCourseModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentsToLecturecise(StudentsAddToCourseModel model)
        {
            var lectureciceId = model.LectureciseId;

            if (model.StudentsInfo.Count() > 0 && lectureciceId != null && this.lectureciseService.Any(lectureciceId))
            {
                var lecturecise = this.lectureciseService.GetByOriginal(lectureciceId);

                var studentIds = model.StudentsInfo.Select(si => si.Id).ToList();

                var addedCounter = 0;

                foreach (var studentId in studentIds)
                {
                    if (!lecturecise.LectureciseStudents.Any(st => st.StudentId == studentId))
                    {
                        lecturecise.LectureciseStudents.Add(new StudentLecturecise() { StudentId = studentId, LectureciseId = lectureciceId });

                        addedCounter++;
                    }
                }

                await this.lectureciseService.SaveLecturecises();
            }

            return RedirectToAction("CurrentYearLecturecises");
        }


        //For populating dropdown with students by lecturecises
        public async Task<JsonResult> GetStudentsByLecturecise(string lectureciseId)
        {
            var students = await this.userService.GetAllStudentsByLecturecise(lectureciseId);

            var studentList = students
                .Select(s => new { s.Id, Name = $"{s.StudentUniId}: {s.UserInfo.FirstName} {s.UserInfo.FamilyName}" });

            return Json(new SelectList(studentList, "Id", "Name"));
        }

        //Method is added here in case I want to make new filters
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
