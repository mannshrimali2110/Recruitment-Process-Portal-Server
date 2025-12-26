using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Data.Configurations
{
    public class JobSkillDefinitionConfiguration : IEntityTypeConfiguration<JobSkillDefinition>
    {
        public void Configure(EntityTypeBuilder<JobSkillDefinition> builder)
        {
            // Composite Primary Key
            builder.HasKey(js => new { js.PositionID, js.SkillID });

            // Relationships
            builder.HasOne(js => js.JobPosition)
                   .WithMany(j => j.JobSkillDefinitions)
                   .HasForeignKey(js => js.PositionID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(js => js.Skill)
                   .WithMany()
                   .HasForeignKey(js => js.SkillID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
