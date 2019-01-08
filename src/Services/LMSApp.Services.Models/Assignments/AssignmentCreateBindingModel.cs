
using AutoMapper;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.MaterialRelated;
using LMSApp.Data.Models.UserTypes;
using LMSApp.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSApp.Services.Models.Assignments
{
   public class AssignmentCreateBindingModel : IMapTo<Assignment>
    {
        [Required]
        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DueDate { get; set; }

        public AssignmentType AssignmentType { get; set; }

        [Required(ErrorMessage ="Please enter a valid maximum grade from 1 to 100.")]
        [Range(1, 100.00)]
        public decimal MaxGrade { get; set; }

        [Required]
        public string LectureciseId { get; set; }

        [Required]
        public string EducatorId { get; set; }

        public IList<string> StudentIds { get; set; }

    }
}
