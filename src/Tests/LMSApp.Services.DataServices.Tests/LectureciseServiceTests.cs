
using AutoMapper;
using LMSApp.Data;
using LMSApp.Data.Common;
using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Models.Courses;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LMSApp.Services.DataServices.Tests
{
    [Collection("LMSApp_Service_Tests")]
    public class LectureciseServiceTests : IClassFixture<DatabaseLectureciseFixture>
    {
        DatabaseLectureciseFixture dbFixture;

        public LectureciseServiceTests(DatabaseLectureciseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
        }

        [Fact]
        public void AnyReturnsFalseForEmptyList()
        {
            var lectureRepository = new Mock<IRepository<Lecturecise>>();
            lectureRepository.Setup(r => r.All()).Returns(new List<Lecturecise>().AsQueryable());
            var service = new LectureciseService(lectureRepository.Object);

            Assert.False(service.Any("test"));
        }

        [Fact]
        public void AnyReturnsTrueForNonEmptyList()
        {
            var firstLectureciseId = this.dbFixture.dbContext.Lecturecises
                .Where(l => l.IsDeleted == false).First().Id;

            var repository = new DbRepository<Lecturecise>(this.dbFixture.dbContext);
            var service = new LectureciseService(repository);

            Assert.True(service.Any(firstLectureciseId));
        }

        [Fact]
        public void GetAllGetsAllLecturecises()
        {

            BaseServiceTests.Initialize();

            var count = this.dbFixture.dbContext.Lecturecises
                .Where(l => l.IsDeleted == false).Count();

            var repository = new DbRepository<Lecturecise>(this.dbFixture.dbContext);
            var service = new LectureciseService(repository);

            Assert.Equal(count, service.GetAll().Count());
        }

        [Fact]
        public async Task GetByIdGetsTheEntityWithTheGivenId()
        {
            BaseServiceTests.Initialize();

            var ids = this.dbFixture.dbContext.Lecturecises
                .Where(l => l.IsDeleted == false).Select(l => l.Id).ToList();

            var repository = new DbRepository<Lecturecise>(this.dbFixture.dbContext);
            var service = new LectureciseService(repository);

            foreach (var id in ids)
            {
                var result = await service.GetById(id);

                Assert.Equal(id, result.Id);
            }
        }


        [Fact]
        public async Task GetByIdGetsReturnsNullIfIdNotFound()
        {
            BaseServiceTests.Initialize();

            var ids = this.dbFixture.dbContext.Lecturecises
                .Where(l => l.IsDeleted == false).Select(l => l.Id).ToList();

            var repository = new DbRepository<Lecturecise>(this.dbFixture.dbContext);
            var service = new LectureciseService(repository);

            var testId = "notGuid";

            var result = await service.GetById(testId);

            Assert.Null(result);
        }


        [Fact]
        public void GetByCourseIdGetsAllAndOnlyWithTheGivenId()
        {
            var courseId = "test";

            BaseServiceTests.Initialize();

            var countWithId = this.dbFixture.dbContext.Lecturecises
                .Where(l => l.CourseId == courseId && l.IsDeleted == false).Count();

            var repository = new DbRepository<Lecturecise>(this.dbFixture.dbContext);
            var service = new LectureciseService(repository);

            Assert.Equal(countWithId, service.GetByCourseId(courseId).Count());
            Assert.Equal(countWithId, service.GetByCourseId(courseId).Where(l => l.CourseId == courseId).Count());
        }

        [Fact]
        public async Task EditLectureciseChangesAndStoresChanges()
        {
            var options = new DbContextOptionsBuilder<LMSAppContext>()
              .UseInMemoryDatabase(databaseName: "CreateLecturecise_Database2")
              .Options;

            BaseServiceTests.Initialize();

            string lectureciseId = null;

            using (var context = new LMSAppContext(options))
            {
                context.Lecturecises.Add(new Lecturecise()
                {
                    CourseId = "1",
                    Type = LectureciseType.Excercise
                }
                );

                context.SaveChanges();

                lectureciseId = context.Lecturecises.Where(l => l.CourseId == "1").First().Id;
            }

            var newCourseId = "2";

            var lectureciseModel = new LectureciseDetailsViewModel()
            {
                Id = lectureciseId,
                CourseId = newCourseId,
                Type = LectureciseType.Excercise
            };

            using (var context = new LMSAppContext(options))
            {
                var repository = new DbRepository<Lecturecise>(context);
                var service = new LectureciseService(repository);
                await service.EditLecturecise(lectureciseModel);

                Assert.Equal(newCourseId, context.Lecturecises.First().CourseId);
            }
        }

        [Fact]
        public async Task DeleteLectureciseByIdMarksTheObjectAsDeleted()
        {
            var options = new DbContextOptionsBuilder<LMSAppContext>()
              .UseInMemoryDatabase(databaseName: "CreateLecturecise_Database3")
              .Options;

            BaseServiceTests.Initialize();

            string lectureciseId = null;

            using (var context = new LMSAppContext(options))
            {
                context.Lecturecises.Add(new Lecturecise()
                {
                    CourseId = "1",
                    Type = LectureciseType.Excercise,
                    IsDeleted = false
                }
                );

                context.SaveChanges();

                lectureciseId = context.Lecturecises.Where(l => l.CourseId == "1").First().Id;
            }

            using (var context = new LMSAppContext(options))
            {
                var repository = new DbRepository<Lecturecise>(context);
                var service = new LectureciseService(repository);

                await service.DeleteLectureciseById(lectureciseId);

                Assert.True(context.Lecturecises.First().IsDeleted);
            }
        }

        [Fact]
        public async Task DeleteLectureciseByIdReturnsNullIfObjectAlreadyDeleted()
        {
            var options = new DbContextOptionsBuilder<LMSAppContext>()
              .UseInMemoryDatabase(databaseName: "CreateLecturecise_Database3")
              .Options;

            BaseServiceTests.Initialize();

            string lectureciseId = null;

            using (var context = new LMSAppContext(options))
            {
                context.Lecturecises.Add(new Lecturecise()
                {
                    CourseId = "1",
                    Type = LectureciseType.Excercise,
                    IsDeleted = true
                }
                );

                context.SaveChanges();

                lectureciseId = context.Lecturecises.Where(l => l.CourseId == "1").First().Id;
            }

            using (var context = new LMSAppContext(options))
            {
                var repository = new DbRepository<Lecturecise>(context);
                var service = new LectureciseService(repository);

                var courseId = await service.DeleteLectureciseById(lectureciseId);

                Assert.Null(courseId);
            }
        }

        [Fact]
        public void GetAllByStudentReturnsLecturecisesByStudentId()
        {
            BaseServiceTests.Initialize();

            var repository = new DbRepository<Lecturecise>(this.dbFixture.dbContext);
            var service = new LectureciseService(repository);

            Assert.Single(service.GetAllByStudent("student1"));
        }

        [Fact]
        public void GetAllByStudentReturnsLecturecisesByEducatorId()
        {
            BaseServiceTests.Initialize();

            var lectureRepository = new Mock<IRepository<Lecturecise>>();
            lectureRepository.Setup(r => r.All())
                .Returns(
                new List<Lecturecise>()
                {
                    new Lecturecise()
                    {
                        Id = "rand1",
                        CourseId = "rand",
                        Course = new Course(){Name ="N", Year ="2017/2018", Semester = Semester.Summer, Major = Major.Biotechnologies},
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
                    },
                    new Lecturecise()
                    {
                        Id = "rand2",
                        CourseId = "rand",
                        Course = new Course(){Name ="N", Year ="2017/2018", Semester = Semester.Summer, Major = Major.Biotechnologies},
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
                    }}
                .AsQueryable());

            var service = new LectureciseService(lectureRepository.Object);

            Assert.Single(service.GetAllByEducator("edu1"));
        }

        [Fact]
        public async Task CreateAsyncCreatesOneEntity()
        {
            BaseServiceTests.Initialize();

            var repository = new DbRepository<Lecturecise>(dbFixture.dbContext);
            var service = new LectureciseService(repository);

            var lectureciseCount = this.dbFixture.dbContext.Lecturecises.Count();

            await service.CreateAsync(new LectureciseCreateBindingModel()
            {
                CourseId = Guid.NewGuid().ToString(),
                Type = LectureciseType.Excercise
            });

            Assert.Equal(lectureciseCount + 1, dbFixture.dbContext.Lecturecises.Count());
        }

        [Fact]
        public async Task CreateAsyncCreatesEntityWithTheGivenValues()
        {
            var options = new DbContextOptionsBuilder<LMSAppContext>()
               .UseInMemoryDatabase(databaseName: "CreateLecturecise_Database1")
               .Options;

            BaseServiceTests.Initialize();

            using (var dbContext = new LMSAppContext(options))
            {
                var repository = new DbRepository<Lecturecise>(dbContext);
                var service = new LectureciseService(repository);

                await service.CreateAsync(new LectureciseCreateBindingModel()
                {
                    CourseId = "1",
                    Type = LectureciseType.Excercise
                }
                 );

                Assert.Equal("1", dbContext.Lecturecises.FirstOrDefault().CourseId);
                Assert.Equal(LectureciseType.Excercise, dbContext.Lecturecises.FirstOrDefault().Type);
            }
        }

        [Fact]
        public void GetByIdOriginalGetsTheLectureciseWithTheGivenId()
        {
            BaseServiceTests.Initialize();

            var id = "rand1";

            var repository = new DbRepository<Lecturecise>(this.dbFixture.dbContext);
            var service = new LectureciseService(repository);

            var result = service.GetByOriginal(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void GetByIdOriginalReturnsNullIfIdNotFound()
        {
            BaseServiceTests.Initialize();

            var id = "NoSuchLecturecise";

            var repository = new DbRepository<Lecturecise>(this.dbFixture.dbContext);
            var service = new LectureciseService(repository);

            var result = service.GetByOriginal(id);

            Assert.Null(result);
        }
    }
}
