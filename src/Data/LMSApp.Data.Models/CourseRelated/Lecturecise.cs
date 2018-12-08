
using LMSApp.Data.Common;
using LMSApp.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models.CourseRelated
{
    public class Lecturecise : BaseModel<int>
    {
        public Lecturecise()
        {
            this.LectureciseEducators = new List<EducatorLecturecise>();
            this.LectureciseStudents = new List<StudentLecturecise>();
            this.Events = new List<Event>();
            this.StudentTasks = new List<StudentAssignment>();
        }

        [Required]
        public LectureciseType Type { get; set; }

        [Required]
        public WeekTime WeekTime { get; set; }

        public IList<EducatorLecturecise> LectureciseEducators { get; set; }

        public IList<StudentLecturecise> LectureciseStudents { get; set; }

        public IList<Event> Events { get; set; }

        public IList<StudentAssignment> StudentTasks { get; set; }

    }
}
