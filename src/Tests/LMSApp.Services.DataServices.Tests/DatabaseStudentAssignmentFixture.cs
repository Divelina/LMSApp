
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
    public class DatabaseStudentAssignmentFixture : IDisposable
    {
        public LMSAppContext dbContext;

        public DatabaseStudentAssignmentFixture()
        {

            var options = new DbContextOptionsBuilder<LMSAppContext>()
               .UseInMemoryDatabase(databaseName: "CommonStudentAssignment_Database")
               .Options;

            this.dbContext = new LMSAppContext(options);

            InitializeLecturecises().GetAwaiter().GetResult();

            InitializeEducators().GetAwaiter().GetResult();

            InitializeAssignments().GetAwaiter().GetResult();

            InitializeStudents().GetAwaiter().GetResult();

            InitializeStudentAssignments().GetAwaiter().GetResult();
        }

        private async Task InitializeLecturecises()
        {

            this.dbContext.Lecturecises.Add(new Lecturecise()
            {
                Id = "lecturecise1",
                Course = new Course()
                {
                    Id = "course1",
                    Name = "course1",
                    Semester = Semester.Summer,
                    Major = Major.Biotechnologies,
                    Year = "2019/2020"
                },
                Type = LectureciseType.Excercise
            });

            this.dbContext.Lecturecises.Add(new Lecturecise()
            {
                Id = "lecturecise2",
                CourseId = "course1",
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

        private async Task InitializeAssignments()
        {
            var educator = await this.dbContext.Educator.FirstAsync();
            var educatorId = educator.Id;

            this.dbContext.Assignments.Add(new Assignment()
            {
                Id = "Assgn1",
                Name = "Assgn1",
                MaxGrade = 5m,
                DateCreated = DateTime.Today.AddDays(-1),
                EducatorId = educatorId,
                LectureciseId = "lecturecise1"
            });

            this.dbContext.Assignments.Add(new Assignment()
            {
                Id = "Assgn2",
                Name = "Assgn2",
                MaxGrade = 5m,
                DateCreated = DateTime.Today.AddDays(-1),
                EducatorId = educatorId,
                LectureciseId = "lecturecise1"
            });

            await this.dbContext.SaveChangesAsync();
        }

        private async Task InitializeStudents()
        {
            this.dbContext.Students.Add(new Student()
            {
                Id = "student1",
                User = new LMSAppUser() { FirstName = "A", FamilyName = "B" },
                Major = Major.Biotechnologies,
                FacultyName = FacultyOf.Biology
            });

            this.dbContext.Students.Add(new Student()
            {
                Id = "student2",
                User = new LMSAppUser() { FirstName = "C", FamilyName = "D" },
                Major = Major.Biotechnologies,
                FacultyName = FacultyOf.Biology
            });

            await this.dbContext.SaveChangesAsync();
        }

        private async Task InitializeStudentAssignments()
        {
            this.dbContext.StudentAssignments.Add(new StudentAssignment()
            {
                StudentId = "student1",
                AssignmentId = "Assgn1",
                Grade = 5m
            });

            this.dbContext.StudentAssignments.Add(new StudentAssignment()
            {
                StudentId = "student1",
                AssignmentId = "Assgn2",
                Grade = 4m
            });

            this.dbContext.StudentAssignments.Add(new StudentAssignment()
            {
                StudentId = "student2",
                AssignmentId = "Assgn1",
                Grade = 2m
            });

            this.dbContext.StudentAssignments.Add(new StudentAssignment()
            {
                StudentId = "student2",
                AssignmentId = "Assgn2",
                Grade = 3m
            });

            await this.dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
