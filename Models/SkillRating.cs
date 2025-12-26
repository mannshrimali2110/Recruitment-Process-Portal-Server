using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
     
    /// Stores skill-wise ratings for an interview result.
    public class SkillRating
    {
        [Key]
        public int RatingID { get; set; }

        [Required]
        public int ResultID { get; set; }

        [ForeignKey(nameof(ResultID))]
        public InterviewResult? InterviewResult { get; set; }

        [Required]
        public int SkillID { get; set; }

        [ForeignKey(nameof(SkillID))]
        public Skill? Skill { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        public decimal Score { get; set; }

        [MaxLength(255)]
        public string? Comments { get; set; }
        
    }
}
