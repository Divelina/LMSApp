
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

            InitializeEducators().GetAwaiter().GetResult();

            InitializeAssignments().GetAwaiter().GetResult();
            
        }

        private async Task InitializeEducators()
        {
            await this.dbContext.Educator.AddAsync(new Educator()
            {
                FacultyName = FacultyOf.Biology,
                User = new LMSAppUser() { FamilyName = "V", FirstName = "V" }
            });
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
                LectureciseId = lectureciseId
            });
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

            await this.dbContext.Lecturecises.AddAsync(new Lecturecise()
            {
                Id = "rand1",
                CourseId = "rand",
                Course = new Course() { Name = "N", Year = "2017/2018", Semester = Semester.Summer, Major = Major.Biotechnologies },
                Type = LectureciseType.Excercise,
                IsDeleted = false,
                WeekTimes = new List<WeekTime>()
                        {
                            new WeekTime() { DayOfWeek = DayOfWeek.Friday, StartHour = "12"}
                        },
                LectureciseStudents = new List<StudentLecturecise>()
                        {
                            new StudentLecturecise(){ LectureciseId = "rand1", StudentId = "student1"}
                        },
                LectureciseEducators = new List<EducatorLecturecise>()
                        {
                            new EducatorLecturecise()
                            { LectureciseId = "rand1",
                                EducatorId = "edu1",
                                Educator = new Educator(){UserId = "some1", FacultyName = FacultyOf.Biology
                                , User = new LMSAppUser(){FamilyName = "V", FirstName = "V"} }
                            }
                        }
            });

            await this.dbContext.Lecturecises.AddAsync(new Lecturecise()
            {
                Id = "rand2",
                CourseId = "rand",
                Course = new Course() { Name = "N", Year = "2017/2018", Semester = Semester.Summer, Major = Major.Biotechnologies },
                Type = LectureciseType.Excercise,
                IsDeleted = false,
                WeekTimes = new List<WeekTime>()
                        {
                        new WeekTime() { DayOfWeek = DayOfWeek.Friday, StartHour = "12"}
                        },
                LectureciseStudents = new List<StudentLecturecise>()
                        {
                            new StudentLecturecise(){ LectureciseId = "rand2", StudentId = "student2"}
                        },
                LectureciseEducators = new List<EducatorLecturecise>()
                        {
                            new EducatorLecturecise()
                            { LectureciseId = "rand2",
                                EducatorId = "edu2",
                                Educator = new Educator(){UserId = "some2", FacultyName = FacultyOf.Biology
                                , User = new LMSAppUser(){FamilyName = "U", FirstName = "U"} }
                            }
                        }
            });

            await this.dbContext.SaveChangesAsync();
        }


        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
