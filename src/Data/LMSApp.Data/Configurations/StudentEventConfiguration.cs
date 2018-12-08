//App
using LMSApp.Data.Models.CourseRelated;
//Microsoft packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSApp.Data.Configurations
{
    public class StudentEventConfiguration : IEntityTypeConfiguration<StudentEvent>
    {
        public void Configure(EntityTypeBuilder<StudentEvent> builder)
        {

            builder.HasKey(se => new { se.StudentId, se.EventId});

            builder.HasOne(se => se.Student)
                .WithMany(s => s.StudentEvents)
                .HasForeignKey(se => se.StudentId);

            builder.HasOne(se => se.Event)
                .WithMany(e => e.StudentEvents)
                .HasForeignKey(se => se.EventId);
        }
    }
}
