using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
    /// Represents a candidate profile in the recruitment system.
    public class CandidateProfile
    {
        [Key]
        public int CandidateID { get; set; }

        // Optional: if candidate self-registers
        public int? UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public AppUser? User { get; set; }

        // Source of profile creation (CV Parse, Manual, Excel, etc.)
        [Required]
        public int SourceID { get; set; }

        [ForeignKey(nameof(SourceID))]
        public AcquisitionSource? Source { get; set; }

        // Recruiter who created the profile (manual upload)
        public int? CreatedByUserID { get; set; }

        [ForeignKey(nameof(CreatedByUserID))]
        public AppUser? CreatedByUser { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        public string? ResumeFilePath { get; set; }

        public string? ResumeRawText { get; set; }
        public ICollection<CandidateSkill> CandidateSkills { get; set; } = new List<CandidateSkill>();
        public ICollection<JobCandidateLink> JobCandidateLinks { get; set; } = new List<JobCandidateLink>();

    }
}
