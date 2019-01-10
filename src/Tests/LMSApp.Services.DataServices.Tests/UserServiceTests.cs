
using AutoMapper;
using LMSApp.Data;
using LMSApp.Data.Common;
using LMSApp.Data.Models;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Models.Courses;
using LMSApp.Services.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LMSApp.Services.DataServices.Tests
{
    [Collection("LMSApp_Service_Tests")]
    public class UserServiceTests : IClassFixture<DatabaseUserFixture>
    {
        DatabaseUserFixture dbFixture;

        public UserServiceTests(DatabaseUserFixture dbFixture)
        {
            this.dbFixture = dbFixture;

        }

        [Fact]
        public void AnyStudentReturnsFalseForEmptyList()
        {
            var studentRepository = new Mock<IRepository<Student>>();
            studentRepository.Setup(r => r.All()).Returns(new List<Student>().AsQueryable());
            var service = new UserService(null, studentRepository.Object);

            Assert.False(service.AnyStudent(0, FacultyOf.Biology));

        }

        [Fact]
        public void AnyStudentReturnsTrueWhenListContainsStudentWithTheGivenProperties()
        {
            var repository = new DbRepository<Student>(dbFixture.dbContext);
            var service = new UserService(null, repository);

            Assert.True(service.AnyStudent(1, FacultyOf.Biology));
        }

        [Fact]
        public async Task CreateAsyncCreatesOneStudent()
        {
            BaseServiceTests.Initialize();

            var repository = new DbRepository<Student>(dbFixture.dbContext);
            var service = new UserService(null, repository);
           
            var student = this.dbFixture.dbContext.Students
                .Where(s => s.User.FamilyName == "B")
                .First();

            var userId = student.UserId;

            this.dbFixture.dbContext.Students.Remove(student);

            this.dbFixture.dbContext.SaveChanges();

            var studentCount = this.dbFixture.dbContext.Students.Count();

            await service.CreateAsync(new StudentBindingModel()
            {
                UserId = userId,
                StudentUniId = 1,
                FacultyName = FacultyOf.Biology,
                Major = Major.Biotechnologies,
                GroupId = "2"
            });

            Assert.Equal(studentCount + 1, dbFixture.dbContext.Students.Count());
        }

        [Fact]
        public void GetStudentByUserIdGetsTheStudentWithTheGivenUserId()
        {

            var userId = this.dbFixture.dbContext.Users
                .Where(u => u.FamilyName == "A").First().Id;

            var repository = new DbRepository<Student>(this.dbFixture.dbContext);
            var service = new UserService(null,repository);

            var result = service.GetStudentByUserId(userId);

            Assert.Equal(userId, result.UserId);
        }


        [Fact]
        public async Task DeleteStudentByUserIdFlagsEducatorWithTheGivenUserIdAsDeleted()
        {
            var repository = new DbRepository<Student>(dbFixture.dbContext);
            var service = new UserService(null, repository);

            var userId = this.dbFixture.dbContext.Users
               .Where(u => u.FirstName == "B").First().Id;

            await service.DeleteStudentByUserId(userId);

            Assert.True(this.dbFixture.dbContext.Students.Where(e => e.UserId == userId)
                .First().IsDeleted);
        }

        [Fact]
        public async Task UnDeleteStudentByUserIdFlagsEducatorWithTheGivenUserIdAsNotDeleted()
        {
            var repository = new DbRepository<Student>(dbFixture.dbContext);
            var service = new UserService(null, repository);

            var userId = this.dbFixture.dbContext.Users
               .Where(u => u.FirstName == "C").First().Id;

            await service.UnDeleteStudentByUserId(userId);

            Assert.False(this.dbFixture.dbContext.Students.Where(e => e.UserId == userId)
                .First().IsDeleted);
        }
        

    }
}
