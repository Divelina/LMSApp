using LMSApp.Data;
using LMSApp.Data.Models;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApp.Services.DataServices.Tests
{
    public class DatabaseEducatorFixture : IDisposable
    {
        public LMSAppContext dbContext;

        public DatabaseEducatorFixture()
        {
            var options = new DbContextOptionsBuilder<LMSAppContext>()
              .UseInMemoryDatabase(databaseName: "CommonEducator_Database")
              .Options;

            this.dbContext = new LMSAppContext(options);

            InitializeEducators().GetAwaiter().GetResult();

            ClearOneEducatorToHaveOneUser();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        private async Task InitializeEducators()
        {
            await this.dbContext.Educator.AddAsync(new Educator()
            {
                FacultyName = FacultyOf.Biology,
                User = new LMSAppUser() { FamilyName = "V", FirstName = "V" }
            });

            await this.dbContext.Educator.AddAsync(new Educator()
            {
                FacultyName = FacultyOf.Biology,
                User = new LMSAppUser() { FamilyName = "U", FirstName = "U" }
            });

            await this.dbContext.Educator.AddAsync(new Educator()
            {
                FacultyName = FacultyOf.Biology,
                User = new LMSAppUser() { FamilyName = "S", FirstName = "S" },
                IsDeleted = true
            });

            await this.dbContext.SaveChangesAsync();
        }


        private void ClearOneEducatorToHaveOneUser()
        {
            var educator = this.dbContext.Educator
                .Where(ed => ed.User.FirstName == "V").First();

            this.dbContext.Educator.Remove(educator);

            this.dbContext.SaveChanges();
        }

    }
}