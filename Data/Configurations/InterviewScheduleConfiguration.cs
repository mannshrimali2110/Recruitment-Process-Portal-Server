using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Data.Configurations
{
    public class InterviewScheduleConfiguration : IEntityTypeConfiguration<InterviewSchedule>
    {
        public void Configure(EntityTypeBuilder<InterviewSchedule> builder)
        {
            builder.HasKey(i => i.InterviewID);

            builder.HasOne(i => i.JobCandidateLink)
                   .WithMany()
                   .HasForeignKey(i => i.LinkID)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(i => i.InterviewType)
                   .WithMany()
                   .HasForeignKey(i => i.TypeID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
