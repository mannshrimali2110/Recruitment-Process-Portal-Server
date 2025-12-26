using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Data.Configurations
{
    public class StatusChangeLogConfiguration : IEntityTypeConfiguration<StatusChangeLog>
    {
        public void Configure(EntityTypeBuilder<StatusChangeLog> builder)
        {
            builder.HasKey(s => s.LogID);

            builder
                .HasOne(s => s.JobCandidateLink)
                .WithMany(jcl => jcl.StatusChangeLogs)
                .HasForeignKey(s => s.LinkID)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(s => s.OldStatus)
                .WithMany()
                .HasForeignKey(s => s.OldStatusID)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(s => s.NewStatus)
                .WithMany()
                .HasForeignKey(s => s.NewStatusID)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(s => s.ChangedByUser)
                .WithMany()
                .HasForeignKey(s => s.ChangedByUserID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
