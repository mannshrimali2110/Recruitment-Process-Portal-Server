using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Data.Configurations
{
    public class CandidateSkillConfiguration : IEntityTypeConfiguration<CandidateSkill>
    {
        public void Configure(EntityTypeBuilder<CandidateSkill> builder)
        {
            // Composite Primary Key
            builder.HasKey(cs => new { cs.CandidateID, cs.SkillID });

            // Relationships
            builder.HasOne(cs => cs.Candidate)
                   .WithMany(c => c.CandidateSkills)
                   .HasForeignKey(cs => cs.CandidateID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cs => cs.Skill)
                   .WithMany()
                   .HasForeignKey(cs => cs.SkillID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.VerifiedByUser)
                   .WithMany()
                   .HasForeignKey(cs => cs.VerifiedByUserID)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
