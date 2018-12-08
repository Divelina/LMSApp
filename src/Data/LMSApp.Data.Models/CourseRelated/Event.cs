//App
using LMSApp.Data.Common;
using LMSApp.Data.Models.MaterialRelated;
//System
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models.CourseRelated
{
    //Events are separate classes
    //or irregular events - exams, etc.
    public class Event : BaseModel<int>
    {
        public Event()
        {
            this.StudentEvents = new List<StudentEvent>();
            this.EducatorEvents = new List<EducatorEvent>();
            this.Materials = new List<Material>();
        }

        [Required]
        public string Name { get; set; }

        public DateTime? StartTime { get; set; }

        public int LectureciseId { get; set; }
        public virtual Lecturecise Lecturecise{ get; set; }

        public IList<StudentEvent> StudentEvents { get; set; }

        public IList<EducatorEvent> EducatorEvents { get; set; }

        public IList<Material> Materials { get; set; }
    }
}
