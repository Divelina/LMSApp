//App
using LMSApp.Data.Models.Enums;
using LMSApp.Data.Models.UserTypes;

namespace LMSApp.Data.Models.CourseRelated
{
    //Many to Many Educator Event
    //With Role in the event
    public class EducatorEvent
    {
        public string EducatorId { get; set; }
        public virtual Educator Educator { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public EventRole EventRole { get; set; }
    }
}
