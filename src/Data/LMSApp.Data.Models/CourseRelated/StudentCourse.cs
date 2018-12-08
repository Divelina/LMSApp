//App
using LMSApp.Data.Models.UserTypes;

namespace LMSApp.Data.Models.CourseRelated
{
    //Many to many Student Course
    public class StudentCourse
    {
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }

        public string CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
