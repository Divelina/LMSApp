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

namespace LMSApp.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
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
        [Route("Admin/Home/Index")]
        public async Task<IActionResult> Index()
        {
            //TODO - think about possible security problems - someone trying to access this tag 
            //But it can only be another admin?
            //How to prevent?
            //Attach userId in the get??? or
            //Tempdata doesn't work because of redirects

            //var userId = (string)TempData["userId"];
            //var currentUser = await this.userManager.GetUserAsync(this.User);

            //if (currentUser.Id != userId)
            //{
            //    return RedirectToAction("/Home/Index");
            //}
            //else
            //{
            //    //TODO make it ot return with some UserViewModel containg the courses, etc.
            //    //But maybe it's not necessary for the Admin
            //    return View();
            //}

            return View();

        }

        }
}
