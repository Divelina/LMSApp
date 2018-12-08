//App
using LMSApp.Data.Models.CourseRelated;
//Microsoft packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSApp.Data.Configurations
{
    public class EducatorEventConfiguration : IEntityTypeConfiguration<EducatorEvent>
    {
        public void Configure(EntityTypeBuilder<EducatorEvent> builder)
        {
            builder.HasKey(ee => new { ee.EducatorId, ee.EventId });

            builder.HasOne(ee => ee.Educator)
                .WithMany(e => e.Events)
                .HasForeignKey(ee => ee.EducatorId);

            builder.HasOne(ee => ee.Event)
                .WithMany(e => e.EducatorEvents)
                .HasForeignKey(ee => ee.EventId);
        }
    }
}
