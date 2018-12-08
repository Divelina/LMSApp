//App
using LMSApp.Data.Models.CourseRelated;
//Microsoft packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSApp.Data.Configurations
{
    public class StudentLectureciseConfiguration : IEntityTypeConfiguration<StudentLecturecise>
    {
        public void Configure(EntityTypeBuilder<StudentLecturecise> builder)
        {
            builder.HasKey(sl => new { sl.LectureciseId, sl.StudentId });

            builder.HasOne(sl => sl.Lecturecise)
                .WithMany(l => l.LectureciseStudents)
                .HasForeignKey(sl => sl.LectureciseId);

            builder.HasOne(sl => sl.Student)
                .WithMany(s => s.StudentLecturecises)
                .HasForeignKey(sl => sl.StudentId);
        }
    }
}
