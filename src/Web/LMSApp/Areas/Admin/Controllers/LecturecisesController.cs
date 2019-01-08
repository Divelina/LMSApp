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

namespace LMSApp.Areas.Admin.Controllers
{
    public class LecturecisesController : AdminBaseController
    {
        private readonly ILectureciseService lectureciseService;

        public LecturecisesController(
            ILectureciseService lectureciseService)
        {
            this.lectureciseService = lectureciseService;
        }

        //For Educator's only own courses in the current year
        [HttpGet]
        public IActionResult AllLecturecises()
        {
            var lecturecises = this.lectureciseService.GetAll()
                .OrderByDescending(l => l.Held)
                .ToList();

            return View(lecturecises);
        }


        //Only flags it as deleted
        [HttpGet]
        public async Task<IActionResult> Delete(string lectureciseId)
        {
            var courseId = await this.lectureciseService.DeleteLectureciseById(lectureciseId);

            if (courseId != null)
            {
                return RedirectToAction("Edit", "Courses", new { Area = "Admin", courseId = courseId });
            }

            //TODO return some error instead
            return RedirectToAction("All", "Courses");
        }

    }
}
