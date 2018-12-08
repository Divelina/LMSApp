//App
using LMSApp.Data.Models.UserTypes;

namespace LMSApp.Data.Models.MaterialRelated
{
    //Many to many Student Material
    public class StudentMaterial
    {
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public string MaterialId { get; set; }
        public virtual Material Material { get; set; }
    }
}