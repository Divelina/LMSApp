//App
using LMSApp.Data.Models.CourseRelated;
//Microsoft packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSApp.Data.Configurations
{
    public class EducatorCourseConfiguration : IEntityTypeConfiguration<EducatorCourse>
    {
        public void Configure(EntityTypeBuilder<EducatorCourse> builder)
        {
            builder.HasKey(ec => new { ec.CourseId, ec.EducatorId});

            builder.HasOne(ec => ec.Course)
                .WithMany(c => c.CourseEducators)
                .HasForeignKey(ec => ec.CourseId);

            builder.HasOne(ec => ec.Educator)
                .WithMany(e => e.Courses)
                .HasForeignKey(ec => ec.EducatorId);
        }
    }
}
