using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace recruitment_process_portal_server.Models
{
     
    /// Stores the evaluation result provided by an interviewer for a specific interview round.
    public class InterviewResult
    {
        [Key]
        public int ResultID { get; set; }

        [Required]
        public int InterviewID { get; set; }

        [ForeignKey(nameof(InterviewID))]
        public InterviewSchedule? InterviewSchedule { get; set; }

        [Required]
        public int InterviewerUserID { get; set; }

        [ForeignKey(nameof(InterviewerUserID))]
        public AppUser? Interviewer { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal? Score { get; set; }

        [Required]
        [MaxLength(50)]
        public string Outcome { get; set; } = string.Empty; // PASS / FAIL / HOLD

        public string? Feedback { get; set; }
        public AppUser? InterviewerUser { get; set; }
        public ICollection<SkillRating> SkillRatings { get; set; } = new List<SkillRating>();

    }
}
