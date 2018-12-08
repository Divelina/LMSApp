//App
using LMSApp.Data.Models.CourseRelated;
//Microsoft packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSApp.Data.Configurations
{
    public class EducatorLectureciseConfiguration : IEntityTypeConfiguration<EducatorLecturecise>
    {
        public void Configure(EntityTypeBuilder<EducatorLecturecise> builder)
        {
            builder.HasKey(el => new { el.EducatorId, el.LectureciseId });

            builder.HasOne(el => el.Educator)
                .WithMany(e => e.Lecturecises)
                .HasForeignKey(el => el.EducatorId);

            builder.HasOne(el => el.Lecturecise)
                .WithMany(l => l.LectureciseEducators)
                .HasForeignKey(el => el.LectureciseId);
        }
    }
}
