//App
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;

namespace LMSApp.Data.Models.CourseRelated
{
    //Many to many Educator Course
    //With Teaching Role
    public class EducatorCourse
    {
        public string EducatorId { get; set; }
        public virtual Educator Educator { get; set; }

        public string CourseId { get; set; }
        public virtual Course Course { get; set; }

        public TeachingRole TeachingRole { get; set; }
    }
}
