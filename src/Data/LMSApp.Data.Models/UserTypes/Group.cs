//App
using LMSApp.Data.Common;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.Enums;
//System
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models.UserTypes
{
    //Grops in which students are
    //Added for better sorting
    //Does not store many things like common assignments, etc.
    //Mind that students can go to events not with their group...
    public class Group : BaseModel<string>
    {
        public Group()
        {
            this.Students = new List<Student>();
            this.GroupEvents = new List<Event>();
            this.Majors = new List<Major>();
        }

        [Required]
        public int Number { get; set; }

        public List<Major> Majors { get; set; }

        public IList<Student> Students { get; set; }

        public IList<Event> GroupEvents { get; set; }
    }
}
