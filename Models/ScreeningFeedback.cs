using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
     
    /// Represents resume screening feedback for a job application.
    public class ScreeningFeedback
    {
        [Key]
        public int FeedbackID { get; set; }

        [Required]
        public int LinkID { get; set; }

        [ForeignKey(nameof(LinkID))]
        public JobCandidateLink JobCandidateLink { get; set; } = null!;

        [Required]
        public int ReviewerUserID { get; set; }

        [ForeignKey(nameof(ReviewerUserID))]
        public AppUser ReviewerUser { get; set; } = null!;

        [Required]
        public string Comments { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string ScreeningDecision { get; set; } = string.Empty;

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}
