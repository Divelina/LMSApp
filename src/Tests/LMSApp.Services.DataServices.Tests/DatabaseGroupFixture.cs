
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
    public class DatabaseGroupFixture : IDisposable
    {
        public LMSAppContext dbContext;

        public DatabaseGroupFixture()
        {

            var options = new DbContextOptionsBuilder<LMSAppContext>()
               .UseInMemoryDatabase(databaseName: "CommonGroup_Database")
               .Options;

            this.dbContext = new LMSAppContext(options);

            InitializeGroups().GetAwaiter().GetResult();
            
        }

        private async Task InitializeGroups()
        {

            this.dbContext.Groups.Add(new Group()
            {
                Number = 1,
                Major = Major.Mixed
            });

            this.dbContext.Groups.Add(new Group()
            {
                Number = 2,
                Major = Major.Biotechnologies
            });

            await this.dbContext.SaveChangesAsync();
        } 

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
