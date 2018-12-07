
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
            this.StudentTasks = new List<StudentTask>();
        }

        [Required]
        public LectureciseType Type { get; set; }

        [Required]
        public WeekTime WeekTime { get; set; }

        public List<EducatorLecturecise> LectureciseEducators { get; set; }

        public List<StudentLecturecise> LectureciseStudents { get; set; }

        public List<Event> Events { get; set; }

        public List<StudentTask> StudentTasks { get; set; }

    }
}
