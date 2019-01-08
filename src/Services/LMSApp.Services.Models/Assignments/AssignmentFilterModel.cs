
using System;

namespace LMSApp.Services.Models.Assignments
{
    public class AssignmentFilterModel
    {
        public string NameStr { get; set; }

        public DateTime? DateCreated { get; set; }

        public string AssignmentType { get; set; }

        public string LectureciseId { get; set; }

        public string IsTheSameEducator { get; set; }
    }
}
