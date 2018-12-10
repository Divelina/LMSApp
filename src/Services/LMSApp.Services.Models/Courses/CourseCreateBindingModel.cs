
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Services.Mapping;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Services.Models.Courses
{
    public class CourseCreateBindingModel : IMapTo<Course>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Semester Semester { get; set; }

        [Required]
        [RegularExpression(@"[0-9]{4}\/[0-9]{4}", ErrorMessage = "Year should be in the format 2018/2019!")]
        public string Year { get; set; }

        [Required]
        public Major Major { get; set; }

        public string MajorDescription { get; set; }

        public string Description { get; set; }
    }
}
