//App
using LMSApp.Data.Common;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.Enums;
//System
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models.CourseRelated
{
    //Common object for lecture and exercise
    //General weekly classes
    public class Lecturecise : BaseModel<string>
    {
        public Lecturecise()
        {
            this.LectureciseEducators = new List<EducatorLecturecise>();
            this.LectureciseStudents = new List<StudentLecturecise>();
            this.WeekTimes = new List<WeekTime>();
            this.Events = new List<Event>();
            this.StudentTasks = new List<StudentAssignment>();
        }

        [Required]
        public LectureciseType Type { get; set; }

        public IList<WeekTime> WeekTimes { get; set; }

        public IList<EducatorLecturecise> LectureciseEducators { get; set; }

        public IList<StudentLecturecise> LectureciseStudents { get; set; }

        public IList<Event> Events { get; set; }

        public IList<StudentAssignment> StudentTasks { get; set; }

        public string CourseId { get; set; }
        public virtual Course Course { get; set; }

    }
}
