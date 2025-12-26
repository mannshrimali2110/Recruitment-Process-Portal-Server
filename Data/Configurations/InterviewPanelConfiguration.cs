using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Data.Configurations
{
    public class InterviewPanelConfiguration : IEntityTypeConfiguration<InterviewPanel>
    {
        public void Configure(EntityTypeBuilder<InterviewPanel> builder)
        {
            // Composite Primary Key
            builder.HasKey(ip => new { ip.InterviewID, ip.InterviewerUserID });

            // Relationship: InterviewSchedule (1) -> InterviewPanel (Many)
            builder
                .HasOne(ip => ip.InterviewSchedule)
                .WithMany(i => i.InterviewPanels)
                .HasForeignKey(ip => ip.InterviewID)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: AppUser (Interviewer) (1) -> InterviewPanel (Many)
            builder
                .HasOne(ip => ip.InterviewerUser)
                .WithMany()
                .HasForeignKey(ip => ip.InterviewerUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ip => ip.PanelRole)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
