using LMSApp.Data.Common;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models.UserTypes
{
    public class Student : BaseModel<string>
    {
        public Student()
        {
            this.StudentCourses = new List<StudentCourse>();
            this.StudentLecturecises = new List<StudentLecturecise>();
            this.StudentTasks = new List<StudentAssignment>();
            this.StudentBadges = new List<StudentBadge>();
        }

        [Required]
        public int UserId { get; set; }
        public virtual LMSAppUser User { get; set; }

        [Required]
        public int StudentUniId { get; set; }

        [Required]
        public Major Major { get; set; }

        [Required]
        public int GroupNumber { get; set; }

        public DateTime? LastSeen { get; set; }

        public IList<StudentCourse> StudentCourses { get; set; }

        public IList<StudentLecturecise> StudentLecturecises { get; set; }

        public IList<StudentAssignment> StudentTasks { get; set; }

        public IList<StudentBadge> StudentBadges { get; set; }


        //TODO - to make these to be calculated - how and when. In the constructor, cause I want them in Db.
        public decimal TaskCompletionRating { get; set; }

        public decimal TaskGradeRating { get; set; }

        //TODO - to make these to be calculated - not to go into database. Only with get is ok?

        public decimal TotalGradeLectures { get; set; }

        public decimal TotalGradeExcercise { get; set; }

    }
}
