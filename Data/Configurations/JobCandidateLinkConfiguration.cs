using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Data.Configurations
{
       public class JobCandidateLinkConfiguration : IEntityTypeConfiguration<JobCandidateLink>
       {
              public void Configure(EntityTypeBuilder<JobCandidateLink> builder)
              {
                     builder.HasKey(j => j.LinkID);

                     builder.HasOne(j => j.JobPosition)
                            .WithMany()
                            .HasForeignKey(j => j.PositionID)
                            .OnDelete(DeleteBehavior.Restrict);

                     builder.HasOne(jcl => jcl.CandidateProfile)
                            .WithMany()
                            .HasForeignKey(jcl => jcl.CandidateID)
                            .OnDelete(DeleteBehavior.Restrict);

                     builder.HasOne(jcl => jcl.ApplicationStatus)
                            .WithMany()
                            .HasForeignKey(jcl => jcl.CurrentStatusID)
                            .OnDelete(DeleteBehavior.Restrict);

                     builder.HasOne(j => j.LinkedByUser)
                            .WithMany()
                            .HasForeignKey(j => j.LinkedByUserID)
                            .OnDelete(DeleteBehavior.SetNull);
              }
       }
}
