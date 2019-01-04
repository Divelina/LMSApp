
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Mapping;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Services.Models.Courses
{
    public class LectureciseCreateBindingModel : IMapTo<Lecturecise>
    {
        [Required]
        public LectureciseType Type { get; set; }

        [Required]
        public string CourseId { get; set; }

        public string WeekTimes { get; set; }

        public string EducatorNames { get; set; }
    }
}
