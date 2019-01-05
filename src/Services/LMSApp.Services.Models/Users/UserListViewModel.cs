
using AutoMapper;
using LMSApp.Data.Models;
using LMSApp.Services.Mapping;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Services.Models.Users 
{
    public class UserListViewModel : IMapFrom<LMSAppUser>
    {
        public UserListViewModel()
        {
            this.IsAdmin = false;
            this.IsEducator = false;
            this.IsStudent = false;
        }

        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string FamilyName { get; set; }

        public string UserName { get; set; }

        public bool IsLockedOut { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsStudent { get; set; }

        public bool IsEducator { get; set; }

    }
}
