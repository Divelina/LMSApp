
using LMSApp.Data;
using LMSApp.Data.Models;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMSApp.Services.DataServices.Tests
{
    public class DatabaseUserFixture : IDisposable
    {
        public LMSAppContext dbContext;

        public DatabaseUserFixture()
        {

            var options = new DbContextOptionsBuilder<LMSAppContext>()
               .UseInMemoryDatabase(databaseName: "CommonUser_Database")
               .Options;

            this.dbContext = new LMSAppContext(options);

            InitializeStudents().GetAwaiter().GetResult();

            InitializeEducators().GetAwaiter().GetResult();

            InitializeGroups();

        }

        private void InitializeGroups()
        {

            this.dbContext.Groups.Add(new Group()
            {
                Id = "1",
                Number = 1,
                Major = Major.Mixed
            });

            this.dbContext.Groups.Add(new Group()
            {
                Id = "2",
                Number = 2,
                Major = Major.Biotechnologies
            });

            this.dbContext.Groups.Add(new Group()
            {
                Id = "3",
                Number = 3,
                Major = Major.MolecularBiology
            });

            dbContext.SaveChanges();
        }

        private async Task InitializeStudents()
        {
            await this.dbContext.Students.AddAsync(new Student()
            {
                Id = "student1",
                StudentUniId = 1,
                Major = Major.MolecularBiology,
                FacultyName = FacultyOf.Biology,
                User = new LMSAppUser() { FamilyName = "A", FirstName = "A" }
            });

            await this.dbContext.Students.AddAsync(new Student()
            {
                Id = "student2",
                StudentUniId = 1,
                Major = Major.MolecularBiology,
                FacultyName = FacultyOf.Biology,
                User = new LMSAppUser() { FamilyName = "B", FirstName = "B" }
            });

            await this.dbContext.Students.AddAsync(new Student()
            {
                Id = "student3",
                StudentUniId = 1,
                Major = Major.MolecularBiology,
                FacultyName = FacultyOf.Biology,
                IsDeleted = true,
                User = new LMSAppUser() { FamilyName = "C", FirstName = "C" }
            });

            this.dbContext.SaveChanges();
        }

        private async Task InitializeEducators()
        {
            await this.dbContext.Educator.AddAsync(new Educator()
            {
                FacultyName = FacultyOf.Biology,
                User = new LMSAppUser() { FamilyName = "V", FirstName = "V" }
            });

            this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
