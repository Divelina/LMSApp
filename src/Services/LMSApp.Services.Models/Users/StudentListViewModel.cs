
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Mapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Services.Models.Users
{
    public class StudentListViewModel : IMapFrom<Student>
    {
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int StudentUniId { get; set; }

        [Required]
        public Major Major { get; set; }

        [Required]
        public FacultyOf FacultyName { get; set; }

        public string GroupId { get; set; }

        public int GroupNumber { get; set; }

        public DateTime? LastSeen { get; set; }

        public UserListViewModel UserInfo { get; set; }
    }
}
