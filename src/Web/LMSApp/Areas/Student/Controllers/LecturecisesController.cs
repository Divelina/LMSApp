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

namespace LMSApp.Areas.Student.Controllers
{
    public class LecturecisesController : StudentBaseController
    {
        private readonly ILectureciseService lectureciseService;
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly UserManager<LMSAppUser> userManager;

        public LecturecisesController(
            ILectureciseService lectureciseService,
            ICourseService courseService,
            IUserService userService,
            UserManager<LMSAppUser> userManager)
        {
            this.lectureciseService = lectureciseService;
            this.courseService = courseService;
            this.userService = userService;
            this.userManager = userManager;
        }

        //For Students's only own courses in the current year
        [HttpGet]
        public async Task<IActionResult> AllLecturecises()
        {
            var studentLecturecises = await GetLecturecisesByCurrentStudentUser();

            return View(studentLecturecises);
        }

        //For Students's own courses in the current year
        [HttpGet]
        public async Task<IActionResult> CurrentYearLecturecises()
        {
            var studentLecturecises = await GetLecturecisesByCurrentStudentUser();

            var coursesIdThisYear = this.courseService.GetAll()
                .Where(c => c.Year.Contains(DateTime.Now.Year.ToString()))
                .Select(c => c.Id)
                .ToList();

            var currentLecturecises = studentLecturecises
                .Where(l => coursesIdThisYear.Contains(l.CourseId)).ToList();

            return View(currentLecturecises);
        }

        private async Task<IList<LectureciseShortViewModel>> GetLecturecisesByCurrentStudentUser()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userId = user.Id;

            var student = this.userService.GetStudentByUserId(userId);

            if (student == null)
            {
                return new List<LectureciseShortViewModel>();
            }

            var studentLecturecises = this.lectureciseService.GetAllByStudent(student.Id)
                .OrderByDescending(l => l.Held).ToList();

            return studentLecturecises;
        }

    }
}
