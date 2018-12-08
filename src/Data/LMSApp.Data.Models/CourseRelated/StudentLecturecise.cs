
using LMSApp.Data.Models.UserTypes;

namespace LMSApp.Data.Models.CourseRelated
{
    public class StudentLecturecise
    {
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int LectureciseId { get; set; }
        public virtual Lecturecise Lecturecise { get; set; }
    }
}
