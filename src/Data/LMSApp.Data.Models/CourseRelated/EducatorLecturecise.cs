
using LMSApp.Data.Models.UserTypes;

namespace LMSApp.Data.Models.CourseRelated
{
    public class EducatorLecturecise
    {
        public string EducatorId { get; set; }
        public virtual Educator Educator { get; set; }

        public int LectureciseId { get; set; }
        public virtual Lecturecise Lecturecise { get; set; }
    }
}
