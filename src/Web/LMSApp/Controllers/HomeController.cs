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

namespace LMSApp.Controllers
{
    public class HomeController : Controller
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

        public async Task<IActionResult> Index()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            if (currentUser != null)
            {

                if (await userManager.IsInRoleAsync(currentUser, "Admin"))
                {

                    TempData["userId"] = currentUser.Id;

                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Contact()
        {
            //var currentUser = await this.userManager.GetUserAsync(this.User);
            //await this.userManager.AddToRoleAsync(currentUser, "Admin");
            //await this.roleManager.CreateAsync(new IdentityRole("Admin"));
            //return this.Ok();

            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
