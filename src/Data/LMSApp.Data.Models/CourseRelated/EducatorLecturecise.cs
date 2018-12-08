//App
using LMSApp.Data.Models.UserTypes;

namespace LMSApp.Data.Models.CourseRelated
{
    //Many to many Educator Lecturecise
    public class EducatorLecturecise
    {
        public string EducatorId { get; set; }
        public virtual Educator Educator { get; set; }

        public string LectureciseId { get; set; }
        public virtual Lecturecise Lecturecise { get; set; }
    }
}
