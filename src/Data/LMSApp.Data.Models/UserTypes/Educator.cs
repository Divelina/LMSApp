//App
using LMSApp.Data.Common;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.MaterialRelated;
//System
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models.UserTypes
{
    //Additional object to store additional properties
    //Intended to have the Educator Role
    public class Educator : BaseModel<string>
    {
        public Educator()
        {
            this.Courses = new List<EducatorCourse>();
            this.Lecturecises = new List<EducatorLecturecise>();
            this.Events = new List<EducatorEvent>();
            this.AssignmentsGiven = new List<Assignment>();
            this.AssignmentsGraded = new List<StudentAssignment>();
            this.MaterialsGiven = new List<Material>();
        }

        [Required]
        public string UserId { get; set; }
        public virtual LMSAppUser User { get; set; }

        [Required]
        public FacultyOf FacultyName { get; set; }

        public string PersonalPageLink { get; set; }

        public string Info { get; set; }

        public IList<EducatorCourse> Courses { get; set; }

        public IList<EducatorLecturecise> Lecturecises { get; set; }

        public IList<EducatorEvent> Events { get; set; }

        public IList<Assignment> AssignmentsGiven { get; set; }

        public IList<StudentAssignment> AssignmentsGraded { get; set; }

        public IList<Material> MaterialsGiven { get; set; }
    }
}
