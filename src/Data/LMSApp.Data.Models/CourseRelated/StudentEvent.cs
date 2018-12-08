//App
using LMSApp.Data.Models.UserTypes;

namespace LMSApp.Data.Models.CourseRelated
{
    //Many to many Student Event
    public class StudentEvent
    {
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public string EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
