//App
using LMSApp.Data.Models.GradeAwards;
//Microsoft packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSApp.Data.Configurations
{
    public class StudentBadgeConfiguration : IEntityTypeConfiguration<StudentBadge>
    {
        public void Configure(EntityTypeBuilder<StudentBadge> builder)
        {
            builder.HasKey(sb => new { sb.BadgeId, sb.StudentId});

            builder.HasOne(sb => sb.Badge)
                .WithMany(b => b.StudentsAwarded)
                .HasForeignKey(sb => sb.BadgeId);

            builder.HasOne(sb => sb.Student)
                .WithMany(s => s.StudentBadges)
                .HasForeignKey(sb => sb.StudentId);
        }
    }
}
