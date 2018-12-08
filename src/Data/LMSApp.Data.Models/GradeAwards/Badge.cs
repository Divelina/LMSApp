//App
using LMSApp.Data.Common;
using LMSApp.Data.Models.AssignmentRelated;
//System
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models.GradeAwards
{
    public class Badge : BaseModel<string>
    {
        public Badge()
        {
            this.StudentsAwarded = new List<StudentBadge>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        public IList<StudentBadge> StudentsAwarded { get; set; }

        //TODO - badge image
    }
}
