//App
using LMSApp.Data.Common;
using LMSApp.Data.Models.AssignmentRelated;
using LMSApp.Data.Models.CourseRelated;
using LMSApp.Data.Models.UserTypes;
//System
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models.MaterialRelated
{
    //Object for materials uploaded by educators
    //TODO revise for materials stored in servers
    public class Material : BaseModel<string>
    {
        public Material()
        {
            this.StudentMaterials = new List<StudentMaterial>();
        }

        [Required]
        public string Name { get; set; }

        public string Content { get; set; }

        //TODO - Remake it according to Storage used
        public string UrlLink { get; set; }

        public DateTime LastChangeDate { get; set; }

        public string EducatorId { get; set; }
        public virtual Educator Educator { get; set; }

        public string CourseId { get; set; }
        public virtual Course Course { get; set; }

        public string AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        public string EventId { get; set; }
        public virtual Event Event{ get; set; }

        public virtual IList<StudentMaterial> StudentMaterials { get; set; }

    }
}
