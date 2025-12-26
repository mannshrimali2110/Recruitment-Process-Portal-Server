using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using recruitment_process_portal_server.Models;

namespace recruitment_process_portal_server.Data.Configurations
{
    public class OfferLetterConfiguration : IEntityTypeConfiguration<OfferLetter>
    {
        public void Configure(EntityTypeBuilder<OfferLetter> builder)
        {
            builder.HasKey(o => o.OfferID);

            builder
                .HasOne(o => o.JobCandidateLink)
                .WithMany(jcl => jcl.OfferLetters)
                .HasForeignKey(o => o.LinkID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
