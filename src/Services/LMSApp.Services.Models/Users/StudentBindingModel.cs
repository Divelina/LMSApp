
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Services.Models.Users
{
    public class StudentBindingModel : IMapTo<Student>
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string GroupId { get; set; }

        [Required]
        public int StudentUniId { get; set; }

        [Required]
        public Major Major { get; set; }

        [Required]
        public FacultyOf FacultyName { get; set; }
    }
}
