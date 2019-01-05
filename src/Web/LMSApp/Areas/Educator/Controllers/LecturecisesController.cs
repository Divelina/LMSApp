using AutoMapper;
using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.CommonInterfaces;
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
        private UserManager<LMSAppUser> userManager;

        public LecturecisesController(ILectureciseService lectureciseService, UserManager<LMSAppUser> userManager, ICourseService courseService)
        {
            this.lectureciseService = lectureciseService;
            this.courseService = courseService;
            this.userManager = userManager;
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
