using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LMSApp.Data.Common;
using Microsoft.AspNetCore.Identity;

namespace LMSApp.Data.Models
{
    // Add profile data for application users by adding properties to the LMSAppUser class
    public class LMSAppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string FamilyName { get; set; }

        //TODO Option to add a photo
    }
}
