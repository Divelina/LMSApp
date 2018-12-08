//App
using LMSApp.Data.Models.AssignmentRelated;
//Microsoft package
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSApp.Data.Configurations
{
    public class StudentAssignmentConfiguration : IEntityTypeConfiguration<StudentAssignment>
    {
        public void Configure(EntityTypeBuilder<StudentAssignment> builder)
        {
            builder.HasKey(sa => new { sa.AssignmentId, sa.StudentId });

            builder.HasOne(sa => sa.Assignment)
                .WithMany(a => a.StudentsAssignedTo)
                .HasForeignKey(sa => sa.AssignmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sa => sa.Student)
                .WithMany(s => s.StudentAssignments)
                .HasForeignKey(sa => sa.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

                
        }
    }
}
