using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recruitment_process_portal_server.Models;

public class ScreeningFeedbackConfiguration : IEntityTypeConfiguration<ScreeningFeedback>
{
    public void Configure(EntityTypeBuilder<ScreeningFeedback> builder)
    {
        builder.HasKey(sf => sf.FeedbackID);

        builder
            .HasOne(sf => sf.JobCandidateLink)
            .WithMany(jcl => jcl.ScreeningFeedbacks)
            .HasForeignKey(sf => sf.LinkID)
            .OnDelete(DeleteBehavior.NoAction);


        builder
            .HasOne(sf => sf.ReviewerUser)
            .WithMany()
            .HasForeignKey(sf => sf.ReviewerUserID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
