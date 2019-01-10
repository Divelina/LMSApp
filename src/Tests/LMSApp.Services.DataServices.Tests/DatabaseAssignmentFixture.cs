
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
    public class DatabaseAssignmentFixture : IDisposable
    {
        public LMSAppContext dbContext;

        public DatabaseAssignmentFixture()
        {

            var options = new DbContextOptionsBuilder<LMSAppContext>()
               .UseInMemoryDatabase(databaseName: "CommonAssignment_Database")
               .Options;

            this.dbContext = new LMSAppContext(options);

            InitializeLecturecises().GetAwaiter().GetResult();

            InitializeCourses().GetAwaiter().GetResult();

            InitializeEducators().GetAwaiter().GetResult();

            InitializeAssignments().GetAwaiter().GetResult();
            
        }

        private async Task InitializeLecturecises()
        {

            await this.dbContext.Lecturecises.AddAsync(new Lecturecise()
            {
                CourseId = "test",
                Type = Data.Models.Enums.LectureciseType.Excercise,
                IsDeleted = false
            });

            await this.dbContext.SaveChangesAsync();
        }

        private async Task InitializeCourses()
        {
            await this.dbContext.Courses.AddAsync(new Course()
            {
                Id = "test",
                Name = "Crs1",
                Semester = Semester.Summer,
                Year = "2018/2019",
                Major = Major.Biotechnologies
            });

            await this.dbContext.SaveChangesAsync();
        }

        private async Task InitializeEducators()
        {
            await this.dbContext.Educator.AddAsync(new Educator()
            {
                FacultyName = FacultyOf.Biology,
                User = new LMSAppUser() { FamilyName = "V", FirstName = "V" }
            });

            await this.dbContext.SaveChangesAsync();
        }

        private async Task InitializeAssignments()
        {
            var lecturecise = await this.dbContext.Lecturecises.FirstAsync();
            var lectureciseId = lecturecise.Id;

            var educator = await this.dbContext.Educator.FirstAsync();
            var educatorId = educator.Id;

            await this.dbContext.Assignments.AddAsync(new Assignment()
            {
                Name = "Assgn1",
                MaxGrade = 10m,
                EducatorId = educatorId,
                LectureciseId = lectureciseId,
                StudentsAssignedTo = new List<StudentAssignment>()
            });

            await this.dbContext.Assignments.AddAsync(new Assignment()
            {
                Name = "Assgn2",
                MaxGrade = 10m,
                EducatorId = educatorId,
                LectureciseId = lectureciseId,
                StudentsAssignedTo = new List<StudentAssignment>()
            });

            await this.dbContext.Assignments.AddAsync(new Assignment()
            {
                Name = "Assgn3",
                MaxGrade = 10m,
                EducatorId = educatorId,
                Lecturecise = new Lecturecise() {
                    CourseId = "test2",
                    Type = Data.Models.Enums.LectureciseType.Excercise,
                    IsDeleted = false
                },
                StudentsAssignedTo = new List<StudentAssignment>()
            });

            await this.dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
