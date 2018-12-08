//App
using LMSApp.Data.Common;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.GradeAwards;
using LMSApp.Data.Models.MaterialRelated;
//System
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSApp.Data.Models.UserTypes
{
    //Additional object to store additional properties
    //Intended to have the Student Role
    public class Student : BaseModel<string>
    {
        public Student()
        {
            this.StudentCourses = new List<StudentCourse>();
            this.StudentLecturecises = new List<StudentLecturecise>();
            this.StudentAssignments = new List<StudentAssignment>();
            this.StudentMaterials = new List<StudentMaterial>();
            this.StudentEvents = new List<StudentEvent>();
            this.StudentBadges = new List<StudentBadge>();
        }

        [Required]
        public string UserId { get; set; }
        public virtual LMSAppUser User { get; set; }

        [Required]
        public int StudentUniId { get; set; }

        [Required]
        public Major Major { get; set; }

        public string GroupId { get; set; }
        public virtual Group Group { get; set; }

        public DateTime? LastSeen { get; set; }

        public IList<StudentCourse> StudentCourses { get; set; }

        public IList<StudentLecturecise> StudentLecturecises { get; set; }

        public IList<StudentAssignment> StudentAssignments { get; set; }

        public IList<StudentMaterial> StudentMaterials { get; set; }

        public IList<StudentEvent> StudentEvents { get; set; }

        public IList<StudentBadge> StudentBadges { get; set; }


        //TODO - to make these to be calculated - how and when. In the constructor, cause I want them in Db.
        [Column(TypeName = "decimal(5, 2)")]
        public decimal TaskCompletionRating { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal TaskGradeRating { get; set; }

        //TODO - to make these to be calculated - not to go into database. Only with get is ok?

        [Column(TypeName = "decimal(5, 2)")]
        public decimal TotalGradeLectures { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal TotalGradeExcercise { get; set; }

    }
}
