using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMSApp.Models;
using Microsoft.AspNetCore.Identity;
using LMSApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using LMSApp.Areas.Admin;

namespace LMSApp.Areas.Educator.Controllers
{
    public class HomeController : EducatorBaseController
    {
        private readonly UserManager<LMSAppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(
            UserManager<LMSAppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


          [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = (string)TempData["userId"];
            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (currentUser.Id != userId)
            {
                return RedirectToAction("/Home/Index");
            }
            else
            {
                //TODO make it ot return with some UserViewModel containg the courses, etc.
                //But maybe it's not necessary for the Admin
                return View();
            }
            
        }

        }
}
