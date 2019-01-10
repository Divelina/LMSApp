
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
    public class CourseServiceTests : IClassFixture<DatabaseCourseFixture>
    {
        DatabaseCourseFixture dbFixture;

        public CourseServiceTests(DatabaseCourseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
        }

        [Fact]
        public async Task CreateAsyncCreatesOneCourse()
        {
            BaseServiceTests.Initialize();

            var repository = new DbRepository<Course>(dbFixture.dbContext);
            var service = new CourseService(repository);

            var courseCount = this.dbFixture.dbContext.Courses.Count();

            await service.CreateAsync(new CourseCreateBindingModel()
            {
                Name = "course1",
                Semester = Semester.Summer,
                Year = "2017/2018",
                Major = Major.Mixed
            });

            Assert.Equal(courseCount + 1, dbFixture.dbContext.Courses.Count());
        }

        [Fact]
        public void AnyCourseReturnsFalseForEmptyList()
        {
            var courseRepository = new Mock<IRepository<Course>>();
            courseRepository.Setup(r => r.All()).Returns(new List<Course>().AsQueryable());
            var service = new CourseService(courseRepository.Object);

            Assert.False(service.AnyCourse("test", Semester.Summer, "test", Major.Mixed));
        }

        [Fact]
        public void AnyCourseReturnsTrueWhenListContainsCourseWithTheGivenProperties()
        {
            var repository = new DbRepository<Course>(dbFixture.dbContext);
            var service = new CourseService(repository);

            Assert.True(service
                .AnyCourse("test1", Semester.Summer, "2010/2011", Major.Biotechnologies));
        }

        [Fact]
        public void AnyCourseReturnsTrueWhenListContainsCourseWithTheGivenId()
        {
            var repository = new DbRepository<Course>(dbFixture.dbContext);
            var service = new CourseService(repository);

            Assert.True(service.AnyCourse("test1"));
        }

        [Fact]
        public void GetAllGetsAllCourses()
        {
            BaseServiceTests.Initialize();

            var count = this.dbFixture.dbContext.Courses
                .Where(l => l.IsDeleted == false).Count();

            var repository = new DbRepository<Course>(this.dbFixture.dbContext);
            var service = new CourseService(repository);

            Assert.Equal(count, service.GetAll().Count());
        }

        [Fact]
        public void GetByIdOriginalGetsTheCourseWithTheGivenId()
        {
            BaseServiceTests.Initialize();

            var id = "test1";

            var repository = new DbRepository<Course>(this.dbFixture.dbContext);
            var service = new CourseService(repository);

            var result = service.GetByIdOriginal(id);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void GetByIdOriginalReturnsNullIfIdNotFound()
        {
            BaseServiceTests.Initialize();

            var id = "NoSuchCourse";

            var repository = new DbRepository<Course>(this.dbFixture.dbContext);
            var service = new CourseService(repository);

            var result = service.GetByIdOriginal(id);

            Assert.Null(result);
        }

        [Fact]
        public void GetByCourseIdGetsCourseWithTheGivenId()
        {
            var courseId = "test1";

            BaseServiceTests.Initialize();

            var countWithId = this.dbFixture.dbContext.Courses
                .Where(c => c.Id == courseId && c.IsDeleted == false).Count();

            var repository = new DbRepository<Course>(this.dbFixture.dbContext);
            var service = new CourseService(repository);

            Assert.Equal(countWithId, service.GetCourseById(courseId).Id);
        }

        [Fact]
        public async Task GetByCourseIdReturnsNullWhenNoCourseWithTheGivenIdExists()
        {
            var courseId = "NoSuchId";

            BaseServiceTests.Initialize();

            var countWithId = this.dbFixture.dbContext.Courses
                .Where(c => c.Id == courseId && c.IsDeleted == false).Count();

            var repository = new DbRepository<Course>(this.dbFixture.dbContext);
            var service = new CourseService(repository);

            Assert.Null(await service.GetCourseById(courseId));
        }


        [Fact]
        public async Task EditCourseByIdChangesCourseAndStoresChanges()
        {
            var options = new DbContextOptionsBuilder<LMSAppContext>()
              .UseInMemoryDatabase(databaseName: "CreateCourse_Database1")
              .Options;

            BaseServiceTests.Initialize();

            string courseId = "edit2";

            using (var context = new LMSAppContext(options))
            {
                context.Courses.Add(new Course()
                    {
                        Id = courseId,
                        Name = "edit2",
                        Semester = Semester.Summer,
                        Year = "2010/2011",
                        Major = Major.Mixed,
                        StudentsInCourse = new List<StudentCourse>()
                });

                context.SaveChanges();
                
            }

            var newYear = "2011/2012";

            var courseModel = new CourseDetailsViewModel()
            {
                Id = courseId,
                Name = "edit2",
                Semester = Semester.Summer,
                Year = newYear,
                Major = Major.Mixed,
                StudentsInCourse = new List<StudentCourse>()
            };

            using (var context = new LMSAppContext(options))
            {
                var repository = new DbRepository<Course>(context);
                var service = new CourseService(repository);
                await service.EditCourseById(courseModel);

                Assert.Equal(newYear, context.Courses.First().Year);
            }
        }

        [Fact]
        public async Task DeleteCourseByIdMarksTheCourseAsDeleted()
        {
            var options = new DbContextOptionsBuilder<LMSAppContext>()
              .UseInMemoryDatabase(databaseName: "CreateCourse_Database2")
              .Options;

            BaseServiceTests.Initialize();

            string courseId = "delete1";

            using (var context = new LMSAppContext(options))
            {
                context.Courses.Add(new Course()
                {
                    Id = courseId,
                    Name = "delete1",
                    Semester = Semester.Summer,
                    Year = "2010/2011",
                    Major = Major.Mixed,
                    StudentsInCourse = new List<StudentCourse>()
                });

                context.SaveChanges();

            }

            using (var context = new LMSAppContext(options))
            {
                var repository = new DbRepository<Course>(context);
                var service = new CourseService(repository);

                await service.DeleteCourseById(courseId);

                Assert.True(context.Courses.First().IsDeleted);
            }
        }

        [Fact] void GetAllByEducatorReturnsAllCoursesOfTheEducatorWithTheGivenId()
        {
            var educatorId = this.dbFixture.dbContext.Educator
                .Where(e => e.User.FamilyName == "V").First().Id;

            BaseServiceTests.Initialize();

            var countCoursesWithEducatorId = this.dbFixture.dbContext.Courses
                .Where(c => c.CourseEducators.Any(e => e.EducatorId == educatorId
                            && c.IsDeleted == false)).Count();

            var repository = new DbRepository<Course>(this.dbFixture.dbContext);
            var service = new CourseService(repository);

            Assert.Equal(countCoursesWithEducatorId,service.GetAllByEducator(educatorId).Count());
        }

    }
}
