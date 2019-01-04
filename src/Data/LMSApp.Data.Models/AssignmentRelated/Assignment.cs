
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
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSApp.Data.Models.AssignmentRelated
{
    public class Assignment : BaseDeletableModel<string>
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

        [Column(TypeName = "decimal(5, 2)")]
        public decimal MaxGrade { get; set; }

        [Required]
        public string LectureciseId { get; set; }
        public virtual Lecturecise Lecturecise { get; set; }

        [Required]
        public string EducatorId { get; set; }
        public virtual Educator Educator { get; set; }

        public string Test { get; set; }

        public virtual IList<Material> Materials { get; set; }

        public virtual IList<StudentAssignment> StudentsAssignedTo { get; set; }

        public virtual IList<Group> GroupsAssignedTo { get; set; }

    }
}
