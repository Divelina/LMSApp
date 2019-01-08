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

namespace LMSApp.Areas.Student.Controllers
{
    public class HomeController : StudentBaseController
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
        [Route("Student/Home/Index")]
        public async Task<IActionResult> Index()
        {
            return View();

        }

    }
}
