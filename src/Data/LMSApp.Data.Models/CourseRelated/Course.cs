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
    public class Course : BaseDeletableModel<string>
    {
        public Course()
        {
            this.Lecturecises = new List<Lecturecise>();
            this.StudentsInCourse = new List<StudentCourse>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public Semester Semester { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        public Major Major { get; set; }

        public string MajorDescription { get; set; }

        public string Description { get; set; }

        public virtual IList<EducatorCourse> CourseEducators { get; set; }

        public virtual IList<Lecturecise> Lecturecises { get; set; }

        public virtual IList<StudentCourse> StudentsInCourse { get; set; }

        public virtual IList<Material> Materials { get; set; }
    }
}

