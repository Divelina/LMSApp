
//App
using LMSApp.Data.Common;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.MaterialRelated;
using LMSApp.Data.Models.UserTypes;

//System
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models.AssignmentRelated
{
    public class Assignment : BaseModel<string>
    {
        public Assignment()
        {
            this.Materials = new List<Material>();
            this.StudentsAssignedTo = new List<StudentAssignment>();
            this.GroupsAssignedTo = new List<Group>();
        }

        [Required]
        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime? DateCreated { get; set; }

        public AssignmentType AssignmentType { get; set; }

        public decimal MaxGrade { get; set; }

        [Required]
        public string LectureciseId { get; set; }
        public virtual Lecturecise Lecturecise { get; set; }

        [Required]
        public string EducatorId { get; set; }
        public virtual Educator Educator { get; set; }

        public IList<Material> Materials { get; set; }

        public IList<StudentAssignment> StudentsAssignedTo { get; set; }

        public IList<Group> GroupsAssignedTo { get; set; }

    }
}
