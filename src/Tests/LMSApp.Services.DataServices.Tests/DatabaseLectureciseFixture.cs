
using LMSApp.Data;
using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.UserTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LMSApp.Services.DataServices.Tests
{
    public class DatabaseLectureciseFixture : IDisposable
    {
        public LMSAppContext dbContext;

        public DatabaseLectureciseFixture()
        {

            var options = new DbContextOptionsBuilder<LMSAppContext>()
               .UseInMemoryDatabase(databaseName: "CommonLecturecise_Database")
               .Options;

            this.dbContext = new LMSAppContext(options);

            InitializeLecturecises().GetAwaiter().GetResult();
            
        }

        private async Task InitializeLecturecises()
        {

           await this.dbContext.Lecturecises.AddAsync(new Lecturecise()
            {
                CourseId = "test",
                Type = Data.Models.Enums.LectureciseType.Excercise,
                IsDeleted = false
            });

            await this.dbContext.Lecturecises.AddAsync(new Lecturecise()
            {
                CourseId = "test",
                Type = Data.Models.Enums.LectureciseType.Excercise,
                IsDeleted = true
            });

           await this.dbContext.Lecturecises.AddAsync(new Lecturecise()
            {
                CourseId = "test1",
                Type = Data.Models.Enums.LectureciseType.Excercise
            });

            await this.dbContext.SaveChangesAsync();
        }


        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
