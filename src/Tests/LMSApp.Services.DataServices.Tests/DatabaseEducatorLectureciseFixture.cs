
using LMSApp.Data;
using LMSApp.Data.Models;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.DataServices.Tests
{
    public class DatabaseEducatorLectureciseFixture : IDisposable
    {
        public LMSAppContext dbContext;

        public DatabaseEducatorLectureciseFixture()
        {

            var options = new DbContextOptionsBuilder<LMSAppContext>()
               .UseInMemoryDatabase(databaseName: "CommonEducatorLecturecise_Database")
               .Options;

            this.dbContext = new LMSAppContext(options);

            InitializeLecturecises().GetAwaiter().GetResult();

            InitializeEducators().GetAwaiter().GetResult();

            InitializeEducatorLecturecises().GetAwaiter().GetResult();
            
        }

        private async Task InitializeLecturecises()
        {

            this.dbContext.Lecturecises.Add(new Lecturecise()
            {
                Id = "lecturecise1",
                Type = LectureciseType.Excercise
            });

            this.dbContext.Lecturecises.Add(new Lecturecise()
            {
                Id = "lecturecise2",
                Type = LectureciseType.Excercise
            });

            await this.dbContext.SaveChangesAsync();
        }

        private async Task InitializeEducators()
        {
            this.dbContext.Educator.Add(new Educator()
            {
                User = new LMSAppUser() { FirstName = "A", FamilyName = "B" },
                FacultyName = FacultyOf.Biology
            });

            await this.dbContext.SaveChangesAsync();
        }

        private async Task InitializeEducatorLecturecises()
        {
            var educator = await this.dbContext.Educator.FirstAsync();
            var educatorId = educator.Id;

            this.dbContext.LectureciseEducators.Add(new EducatorLecturecise()
            {
                LectureciseId = "lecturecise1",
                EducatorId = educatorId
            });

            this.dbContext.LectureciseEducators.Add(new EducatorLecturecise()
            {
                LectureciseId = "lecturecise2",
                EducatorId = educatorId
            });

            await this.dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
