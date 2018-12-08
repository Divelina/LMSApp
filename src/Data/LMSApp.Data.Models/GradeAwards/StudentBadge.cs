//App
using LMSApp.Data.Models.UserTypes;

namespace LMSApp.Data.Models.GradeAwards
{
    //Many to many Student Badge
    public class StudentBadge
    {
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int BadgeId { get; set; }
        public virtual Badge Badge { get; set; }
    }
}
