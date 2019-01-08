
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Services.Models.Users
{
   public class EducatorBindingModel : IMapTo<Educator>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public FacultyOf FacultyName { get; set; }

    }
}
