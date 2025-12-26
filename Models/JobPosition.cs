using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
     
    /// Represents a job opening within the recruitment system.
    public class JobPosition
    {
        [Key]
        public int PositionID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        // Recruiter who created the job
        [Required]
        public int CreatedByUserID { get; set; }

        [ForeignKey(nameof(CreatedByUserID))]
        public AppUser? CreatedByUser { get; set; }

        // Current job lifecycle status (Open, Closed, On Hold)
        [Required]
        public int StatusID { get; set; }

        [ForeignKey(nameof(StatusID))]
        public JobStatus? Status { get; set; }

        // Optional: job linked to a recruitment event
        public int? EventID { get; set; }

        [ForeignKey(nameof(EventID))]
        public RecruitmentEvent? Event { get; set; }

        // Optional: reason when job is closed
        public int? ClosureReasonID { get; set; }

        [ForeignKey(nameof(ClosureReasonID))]
        public ClosureReason? ClosureReason { get; set; }

        // Optional: selected candidate when position is filled
        public int? SelectedCandidateID { get; set; }

        [ForeignKey(nameof(SelectedCandidateID))]
        public CandidateProfile? SelectedCandidate { get; set; }
        public ICollection<JobSkillDefinition> JobSkillDefinitions { get; set; } = new List<JobSkillDefinition>();
        public ICollection<JobCandidateLink> JobCandidateLinks { get; set; } = new List<JobCandidateLink>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
