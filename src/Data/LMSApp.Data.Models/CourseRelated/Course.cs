
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LMSApp.Data.Common;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.MaterialRelated;

namespace LMSApp.Data.Models.CourseRelated
{
    public class Course : BaseModel<string>
    {
        public Course()
        {
            this.Majors = new List<Major>();
            this.Lecturecises = new List<Lecturecise>();
            this.StudentsInCourse = new List<StudentCourse>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public Semester Semester { get; set; }

        [Required]
        public string Year { get; set; }

        public List<Major> Majors { get; set; }

        public List<Lecturecise> Lecturecises { get; set; }

        public List<StudentCourse> StudentsInCourse { get; set; }

        public List<Material> Materials { get; set; }
    }
}

