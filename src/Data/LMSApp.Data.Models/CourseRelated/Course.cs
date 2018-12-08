//App
using LMSApp.Data.Common;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.MaterialRelated;
//System
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models.CourseRelated
{
    //Course is the subject
    //Separate year/semester is separate course
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

        public IList<Major> Majors { get; set; }

        public IList<Lecturecise> Lecturecises { get; set; }

        public IList<StudentCourse> StudentsInCourse { get; set; }

        public IList<Material> Materials { get; set; }
    }
}

