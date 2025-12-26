using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Data.Configurations
{
    public class SkillRatingConfiguration : IEntityTypeConfiguration<SkillRating>
    {
        public void Configure(EntityTypeBuilder<SkillRating> builder)
        {
            // Primary Key
            builder.HasKey(sr => sr.RatingID);

            // Relationship: InterviewResult (1) -> SkillRating (Many)
            builder
                .HasOne(sr => sr.InterviewResult)
                .WithMany(ir => ir.SkillRatings)
                .HasForeignKey(sr => sr.ResultID)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: Skill (1) -> SkillRating (Many)
            builder
                .HasOne(sr => sr.Skill)
                .WithMany()
                .HasForeignKey(sr => sr.SkillID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(sr => sr.Score)
                   .IsRequired();
        }
    }
}
