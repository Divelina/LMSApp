using AutoMapper;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Users;
using Microsoft.AspNetCore.Authorization;
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

        public LecturecisesController(ILectureciseService lectureciseService)
        {
            this.lectureciseService = lectureciseService;
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
