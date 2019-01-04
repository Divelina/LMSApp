using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Courses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApp.Areas.Educator.Controllers
{
    public class CoursesController : EducatorBaseController
    {
        private readonly ICourseService courseService;
        private readonly UserManager<LMSAppUser> userManager;
        private readonly IEducatorService educatorService;

        public CoursesController(ICourseService courseService, UserManager<LMSAppUser> userManager, IEducatorService educatorService)
        {
            this.courseService = courseService;
            this.educatorService = educatorService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (! await this.userManager.IsInRoleAsync(currentUser, "Educator"))
            {
                return RedirectToAction("All", "Courses", new { Area = "Admin" });
            }

            var educatorId = this.educatorService.GetByUserId(currentUser.Id).Id;

            var courses = this.courseService.GetAllByEducator(educatorId)
                .OrderByDescending(c => c.Year).ToList();

            return View(courses);
        }
    }
}
