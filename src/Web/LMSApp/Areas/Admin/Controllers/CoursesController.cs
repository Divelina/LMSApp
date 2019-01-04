﻿using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Courses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApp.Areas.Admin.Controllers
{
    public class CoursesController : AdminBaseController
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
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

           var couresId =  await this.courseService.CreateAsync(course);

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

            return RedirectToAction("Edit", new { courseId = courseModel.Id});
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
    }
}
