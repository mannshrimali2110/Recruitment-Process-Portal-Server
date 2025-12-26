using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
     
    /// Represents a scheduled interview round for a job application.
    public class InterviewSchedule
    {
        [Key]
        public int InterviewID { get; set; }

        [Required]
        public int LinkID { get; set; }

        [ForeignKey(nameof(LinkID))]
        public JobCandidateLink? JobCandidateLink { get; set; }

        [Required]
        public int TypeID { get; set; }

        [ForeignKey(nameof(TypeID))]
        public InterviewType? InterviewType { get; set; }

        [Required]
        public int RoundNumber { get; set; } = 1;

        [Required]
        public DateTime ScheduledStart { get; set; }

        [Required]
        public DateTime ScheduledEnd { get; set; }

        public string? MeetingLink { get; set; }

        public bool IsCompleted { get; set; } = false;
        public ICollection<InterviewPanel> InterviewPanels { get; set; } = new List<InterviewPanel>();

        public ICollection<InterviewResult> InterviewResults { get; set; }
            = new List<InterviewResult>();

    }
}
