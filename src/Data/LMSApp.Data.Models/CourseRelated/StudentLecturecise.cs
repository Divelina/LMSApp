//App
using LMSApp.Data.Models.UserTypes;

namespace LMSApp.Data.Models.CourseRelated
{
    //Many to many Student Lecturecise
    public class StudentLecturecise
    {
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public string LectureciseId { get; set; }
        public virtual Lecturecise Lecturecise { get; set; }
    }
}
