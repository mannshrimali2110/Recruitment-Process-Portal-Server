using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
     
    /// Represents a panel member participating in an interview.
    public class InterviewPanel
    {
        [Required]
        public int InterviewID { get; set; }

        [ForeignKey(nameof(InterviewID))]
        public InterviewSchedule? InterviewSchedule { get; set; }

        [Required]
        public int InterviewerUserID { get; set; }

        [ForeignKey(nameof(InterviewerUserID))]
        public AppUser InterviewerUser { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string PanelRole { get; set; } = "INTERVIEWER";
    }
}
