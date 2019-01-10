
using LMSApp.Data;
using LMSApp.Data.Models;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApp.Services.DataServices.Tests
{
    public class DatabaseCourseFixture : IDisposable
    {
        public LMSAppContext dbContext;

        public DatabaseCourseFixture()
        {

            var options = new DbContextOptionsBuilder<LMSAppContext>()
               .UseInMemoryDatabase(databaseName: "CommonCourse_Database")
               .Options;

            this.dbContext = new LMSAppContext(options);

            InitializeEducators().GetAwaiter().GetResult();

            InitializeLecturecises().GetAwaiter().GetResult();
            
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

            this.dbContext.SaveChanges();
        }


        private async Task InitializeLecturecises()
        {
            var educator1 = this.dbContext.Educator.Where(ed => ed.User.FamilyName == "V").First();
            var educator2 = this.dbContext.Educator.Where(ed => ed.User.FamilyName == "U").First();

            await this.dbContext.Lecturecises.AddAsync(new Lecturecise()
            {
                Course = new Course()
                {
                    Id = "test1",
                    Name = "test1",
                    Semester = Semester.Summer,
                    Year = "2010/2011",
                    Major = Major.Biotechnologies,
                    StudentsInCourse = new List<StudentCourse>(),
                    CourseEducators = new List<EducatorCourse>()
                   {
                       new EducatorCourse() {Educator = educator1, CourseId = "test2"}
                   }
                },
                Type = Data.Models.Enums.LectureciseType.Excercise,
                IsDeleted = false
            });

            await this.dbContext.Lecturecises.AddAsync(new Lecturecise()
            {
                Course = new Course()
                {
                    Id = "test1",
                    Name = "test1",
                    Semester = Semester.Summer,
                    Year = "2010/2011",
                    Major = Major.Biotechnologies,
                    StudentsInCourse = new List<StudentCourse>(),
                    CourseEducators = new List<EducatorCourse>()
                   {
                       new EducatorCourse() {Educator = educator2, CourseId = "test2"}
                   }
                },
                Type = Data.Models.Enums.LectureciseType.Excercise,
                IsDeleted = true
            });

           await this.dbContext.Lecturecises.AddAsync(new Lecturecise()
            {
                Course = new Course()
               {
                   Id = "test2",
                   Name = "test2",
                   Semester = Semester.Summer,
                   Year = "2010/2011",
                   Major = Major.Mixed,
                   StudentsInCourse = new List<StudentCourse>(),
                   CourseEducators = new List<EducatorCourse>()
                   { 
                       new EducatorCourse() {Educator = educator1, CourseId = "test2"},
                       new EducatorCourse() {Educator = educator2, CourseId = "test2"}
                   }
               },
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
