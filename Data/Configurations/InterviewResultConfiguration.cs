using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Data.Configurations
{
    public class InterviewResultConfiguration : IEntityTypeConfiguration<InterviewResult>
    {
        public void Configure(EntityTypeBuilder<InterviewResult> builder)
        {
            // Primary Key
            builder.HasKey(ir => ir.ResultID);

            // Relationship: InterviewSchedule (1) -> InterviewResult (Many)
            builder
                .HasOne(ir => ir.InterviewSchedule)
                .WithMany(i => i.InterviewResults)
                .HasForeignKey(ir => ir.InterviewID)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: AppUser (Interviewer) (1) -> InterviewResult (Many)
            builder
                .HasOne(ir => ir.Interviewer)
                .WithMany()
                .HasForeignKey(ir => ir.InterviewerUserID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ir => ir.Outcome)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
