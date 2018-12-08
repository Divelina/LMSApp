//App
using LMSApp.Data.Models.CourseRelated;
//Microsoft packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSApp.Data.Configurations
{
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(sc => new { sc.CourseId, sc.StudentId });

            builder.HasOne(sc => sc.Course)
                .WithMany(c => c.StudentsInCourse)
                .HasForeignKey(sc => sc.CourseId);

            builder.HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);
        }
    }
}
