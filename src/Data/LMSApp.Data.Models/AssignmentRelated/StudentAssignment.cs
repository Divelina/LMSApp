
//App
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;

//System
using System;

namespace LMSApp.Data.Models.AssignmentRelated
{
    //Many to many StudentAssignment
    //With Grades inside!!!
    public class StudentAssignment
    {
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public string AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        public string LectureciseId { get; set; }
        public virtual Lecturecise Lecturecise { get; set; }

        public DateTime? DueDate { get; set; }

        public AssignmentStatus AssignmentStatus { get; set; }

        public bool Expired { get; set; }

        public string GraderId { get; set; }
        public virtual Educator Grader { get; set; }

        public decimal Grade { get; set; }

        public string GradeComment { get; set; }
    }
}
