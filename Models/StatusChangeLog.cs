using System.ComponentModel.DataAnnotations;

namespace recruitment_process_portal_server.Models
{
     
    /// Tracks every status change in the recruitment lifecycle for auditability.
    public class StatusChangeLog
    {
        [Key]
        public int LogID { get; set; }

        [Required]
        public int LinkID { get; set; } // FK - JobCandidateLink

        [Required]
        public int OldStatusID { get; set; } // FK - ApplicationStatus

        [Required]
        public int NewStatusID { get; set; } // FK - ApplicationStatus

        [Required]
        public int ChangedByUserID { get; set; } // FK - AppUser

        [MaxLength(1000)]
        public string? ReasonText { get; set; } // Mandatory for ON_HOLD

        [Required]
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        public JobCandidateLink JobCandidateLink { get; set; } = null!;
        public ApplicationStatus OldStatus { get; set; } = null!;
        public ApplicationStatus NewStatus { get; set; } = null!;
        public AppUser ChangedByUser { get; set; } = null!;
    }
}
