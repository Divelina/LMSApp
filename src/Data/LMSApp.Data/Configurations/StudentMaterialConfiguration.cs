//App
using LMSApp.Data.Models.MaterialRelated;
//Microsoft packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSApp.Data.Configurations
{
    public class StudentMaterialConfiguration : IEntityTypeConfiguration<StudentMaterial>
    {
        public void Configure(EntityTypeBuilder<StudentMaterial> builder)
        {
            builder.HasKey(sm => new { sm.MaterialId, sm.StudentId });

            builder.HasOne(sm => sm.Material)
                .WithMany(m => m.StudentMaterials)
                .HasForeignKey(sm => sm.MaterialId);

            builder.HasOne(sm => sm.Student)
                .WithMany(s => s.StudentMaterials)
                .HasForeignKey(sm => sm.StudentId);
        }
    }
}
