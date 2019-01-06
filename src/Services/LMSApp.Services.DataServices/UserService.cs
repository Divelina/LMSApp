﻿
using AutoMapper;
using LMSApp.Data.Common;
using LMSApp.Data.Models;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Mapping;
using LMSApp.Services.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApp.Services.DataServices
{
    public class UserService : IUserService
    {
        private UserManager<LMSAppUser> userManager;
        private readonly IRepository<Student> studentRepository;

        public UserService(UserManager<LMSAppUser> userManager, IRepository<Student> studentRepository)
        {
            this.userManager = userManager;
            this.studentRepository = studentRepository;
        }

        public async Task<IList<UserListViewModel>> GetAll()
        {
            var users = this.userManager.Users
                .Select(u => new UserListViewModel()
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    FamilyName = u.FamilyName,
                    UserName = u.UserName
                })
                .ToList();

            foreach (var user in users)
            {
                var userDb = await this.userManager.FindByIdAsync(user.Id);

                user.IsLockedOut = await this.userManager.IsLockedOutAsync(userDb);

                user.IsAdmin = await this.userManager.IsInRoleAsync(userDb, "Admin");

                user.IsEducator = await this.userManager.IsInRoleAsync(userDb, "Educator");

                user.IsStudent = await this.userManager.IsInRoleAsync(userDb, "Student");
            }

            return users;
        }

        public async Task<IList<StudentListViewModel>> GetAllStudents()
        {
            var studentUsers = await this.userManager.GetUsersInRoleAsync("Student");

            var students = this.studentRepository.All()
                .Where(st => st.IsDeleted == false)
                .To<StudentListViewModel>()
                 .ToList();

            students.ForEach(st => st.UserInfo = Mapper.Map<UserListViewModel>(studentUsers.FirstOrDefault(su => su.Id == st.UserId)));

            return students;
        }

        public async Task<IList<StudentListViewModel>> GetAllStudentsByCourse(string courseId)
        {
            var studentUsers = await this.userManager.GetUsersInRoleAsync("Student");

            var students = this.studentRepository.All()
                .Where(st => st.IsDeleted == false && st.StudentCourses.Any(c => c.CourseId == courseId))
                .To<StudentListViewModel>()
                 .ToList();

            students.ForEach(st => st.UserInfo = Mapper.Map<UserListViewModel>(studentUsers.FirstOrDefault(su => su.Id == st.UserId)));

            return students;
        }

        public async Task<IList<StudentListViewModel>> GetAllStudentsByLecturecise(string lectureciseId)
        {
            var studentUsers = await this.userManager.GetUsersInRoleAsync("Student");

            var students = this.studentRepository.All()
                .Where(st => st.IsDeleted == false && st.StudentLecturecises.Any(l => l.LectureciseId == lectureciseId))
                .To<StudentListViewModel>()
                 .ToList();

            students.ForEach(st => st.UserInfo = Mapper.Map<UserListViewModel>(studentUsers.FirstOrDefault(su => su.Id == st.UserId)));

            return students;
        }
    }
}
