using LMSApp.Data.Models;
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

            //TODO - add services that checks for identical course - same name, semester, year and major?
           var couresId =  await this.courseService.CreateAsync(course);

            //TODO - redirect to details page
            return RedirectToAction("Index", "Home", new { Area = "Admin" });
            //return this.RedirectToAction("Details", new { id = id });
        }
    }
}
