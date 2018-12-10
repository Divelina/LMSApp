using LMSApp.Data;
using LMSApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sandbox
{
    public class RoleSeeder
    {
        private readonly LMSAppContext context;
        private readonly UserManager<LMSAppUser> userManager;
        private readonly RoleManager<IdentityRole> rolesManager;

        public RoleSeeder(
            LMSAppContext context,
            UserManager<LMSAppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.rolesManager = roleManager;
        }

        public async Task CreateRolesAsync()
        {

            var adminRole = "Admin";
            var roleNames = new String[] { adminRole, "Educator", "Student"};

            foreach (var roleName in roleNames)
            {
                var role = await rolesManager.RoleExistsAsync(roleName);
                if (!role)
                {
                    var result = await rolesManager.CreateAsync(new IdentityRole { Name = roleName });

                }
            }
            // administrator
            var user = new LMSAppUser
            {
                UserName = "Administrator",
                Email = "something@something.com",
                EmailConfirmed = true,
                FirstName = "Test1",
                FamilyName = "Test2"
            };
            var i = await userManager.FindByEmailAsync(user.Email);
            if (i == null)
            {
                var adminUser = await userManager.CreateAsync(user, "123456");
                if (adminUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, adminRole);
                    
                }
            }

            //educator
            var user2 = new LMSAppUser
            {
                UserName = "Educator",
                Email = "edu@edu.com",
                EmailConfirmed = true,
                FirstName = "Edu1",
                FamilyName = "Edu2"
            };
            var j = await userManager.FindByEmailAsync(user2.Email);
            if (j == null)
            {
                var eduUser = await userManager.CreateAsync(user2, "123456");
                if (eduUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user2, "Educator");
                }
            }

            //student
            var user3 = new LMSAppUser
            {
                UserName = "Student",
                Email = "stu@stu.com",
                EmailConfirmed = true,
                FirstName = "Stu1",
                FamilyName = "Stu2"
            };
            var k = await userManager.FindByEmailAsync(user3.Email);
            if (k == null)
            {
                var eduUser = await userManager.CreateAsync(user3, "123456");
                if (eduUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user3, "Student");
                }
            }
        }
    }
}