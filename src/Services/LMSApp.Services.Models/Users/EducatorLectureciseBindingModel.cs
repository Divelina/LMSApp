
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Services.Mapping;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Services.Models.Users
{
    public class EducatorLectureciseBindingModel : IMapTo<EducatorLecturecise>
    {
        [Required]
        public string LectureciseId { get; set; }

        [Required]
        public string EducatorId { get; set; }
    }
}
