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
        private UserManager<LMSAppUser> userManager;

        public LecturecisesController(
            ILectureciseService lectureciseService,
            IEducatorService educatorService,
            ICourseService courseService,
            IEducatorLectureciseService educatorLectureciseService,
            IWeekTimeService weekTimeService, 
            UserManager<LMSAppUser> userManager)
        {
            this.lectureciseService = lectureciseService;
            this.courseService = courseService;
            this.educatorService = educatorService;
            this.educatorLectureciseService = educatorLectureciseService;
            this.weekTimeService = weekTimeService;
            this.userManager = userManager;
        }

        //For Educator's only own courses in the current year
        [HttpGet]
        public async Task<IActionResult> AllLecturecises()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (await this.userManager.IsInRoleAsync(user, "Admin"))
            {
                return View(this.lectureciseService.GetAll().ToList());
            }

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
                lecturecise.WeekTimes.Remove(weekTime);               
            }

            this.lectureciseService.SaveLecturecises();

            return RedirectToAction("Edit", new { lectureciseId = lectureciseId});
        }

        [HttpGet]
        public IActionResult ClearEducators(string lectureciseId)
        {
            var lecturecise = this.lectureciseService.GetByOriginal(lectureciseId);

            var educatorLecturecises = lecturecise.LectureciseEducators;

            foreach (var educatorLecturecise in educatorLecturecises)
            {
                this.educatorLectureciseService.DeleteById(lectureciseId);
            }

            this.lectureciseService.SaveLecturecises();

            return RedirectToAction("Edit", new { lectureciseId = lectureciseId });
        }

        //Only flags it as deleted
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string lectureciseId)
        {
            var courseId = await this.lectureciseService.DeleteLectureciseById(lectureciseId);

            if (courseId != null)
            {
                return RedirectToAction("Edit", "Courses", new { Area = "Admin", courseId = courseId });
            }

            //TODO return some error instead
            return RedirectToAction("All", "Courses", new { Area = "Admin" });
        }

        //[HttpPost]
        //public async Task<IActionResult> AddEducator(EducatorLectureciseBindingModel model)
        //{
        //    var lecturecise = await this.lectureciseService.GetById(model.LectureciseId);

        //    if (!this.ModelState.IsValid)
        //    {
        //        return RedirectToAction("AddLecturecise", "Courses", new { Area = "Admin", courseId = lecturecise.CourseId });
        //    }

        //    var educatorLecturecise = Mapper.Map<EducatorLecturecise>(model);

        //    lecturecise.LectureciseEducators.Add(educatorLecturecise);

        //   await this.lectureciseService.EditLecturecise(lecturecise);

        //    return RedirectToAction("AddLecturecise", "Courses", new { Area = "Admin", courseId = lecturecise.CourseId });
        //}
    }
}
