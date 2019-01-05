using AutoMapper;
using LMSApp.Data.Models;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Courses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMSApp.Areas.Admin.Controllers
{
    public class CoursesController : AdminBaseController
    {
        private readonly ICourseService courseService;
        private readonly ILectureciseService lectureciseService;

        public CoursesController(ICourseService courseService, ILectureciseService lectureciseService)
        {
            this.courseService = courseService;
            this.lectureciseService = lectureciseService;
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateBindingModel course)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(course);
            }

            if (this.courseService.AnyCourse(course.Name, course.Semester, course.Year, course.Major))
            {

                TempData["Error"] +=
                    "Error: Course with the same name, semester, year and major already in database.";

                return this.View(course);
            }

            var couresId = await this.courseService.CreateAsync(course);

            //TODO - clear the form with JS?
            return RedirectToAction("All", "Courses", new { Area = "Admin" });
        }

        [HttpGet]
        public IActionResult All()
        {
            var courses = this.courseService.GetAll().ToList()
                .OrderByDescending(c => c.Year).ToList();

            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string courseId)
        {

            var courseModel = await this.courseService.GetCourseById(courseId);

            //TODO return Error badrequest;
            //if courseModel == null

            return View(courseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseDetailsViewModel courseModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View(courseModel);
            }

            courseModel.Id = TempData["courseId"].ToString();

            await this.courseService.EditCourseById(courseModel);

            return RedirectToAction("Edit", new { courseId = courseModel.Id });
        }

        //Only flags it as deleted
        [HttpGet]
        public async Task<IActionResult> Delete(string courseId)
        {
            //TODO return Error badrequest;
            //if courseModel == null

            await this.courseService.DeleteCourseById(courseId);

            return RedirectToAction("All", "Courses", new { Area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> AddLecturecise(string courseId)
        {
            var courseModel = await this.courseService.GetCourseById(courseId);

            //TODO return Error badrequest;
            //if courseModel == null

            ViewBag.Course = courseModel;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddLecturecise(LectureciseCreateBindingModel lectureciseModel)
        {

            if (!this.ModelState.IsValid)
            {
                this.View(lectureciseModel);
            }

            var newLectureciseId = await this.lectureciseService.CreateAsync(lectureciseModel);

            if (lectureciseModel.EducatorIds != null || lectureciseModel.Day1 != null || lectureciseModel.Day2 != null)
            {
                var lecturecise = this.lectureciseService.GetByOriginal(newLectureciseId);

                if (lectureciseModel.EducatorIds != null)
                {
                    var course = this.courseService.GetByIdOriginal(lectureciseModel.CourseId);

                    foreach (var educatorId in lectureciseModel.EducatorIds)
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
                    { DayOfWeek = lectureciseModel.Day1.Value, StartHour = lectureciseModel.StartTime1.Value.TimeOfDay.ToString() };

                    if (lectureciseModel.EndTime1 != null)
                    {
                        weekTime1.EndHour = lectureciseModel.EndTime1.Value.TimeOfDay.ToString();
                    }

                    lecturecise.WeekTimes.Add(weekTime1);
                }

                if (lectureciseModel.Day2 != null && lectureciseModel.StartTime2 != null)
                {
                    var weekTime2 = new WeekTime()
                    { DayOfWeek = lectureciseModel.Day2.Value, StartHour = lectureciseModel.StartTime2.Value.TimeOfDay.ToString() };

                    if (lectureciseModel.EndTime2 != null)
                    {
                        weekTime2.EndHour = lectureciseModel.EndTime1.Value.TimeOfDay.ToString();
                    }

                    lecturecise.WeekTimes.Add(weekTime2);
                }

                await this.lectureciseService.SaveLecturecises();

                //await this.lectureciseService.EditLecturecise(lecturecise);
            }

            return RedirectToAction("AddLecturecise", new { courseId = lectureciseModel.CourseId });
        }

        public JsonResult GetLectureciseByCourse(string courseId)
        {
            var lecturecises = this.lectureciseService.GetByCourseId(courseId)
                .Select(l => new { l.Id, Name = $"{l.EducatorNames}; {l.WeekTimes}"});

            return Json(new SelectList(lecturecises, "Id", "Name"));
        }
}
}
