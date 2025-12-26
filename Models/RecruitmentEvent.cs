using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
     
    /// Represents a bulk recruitment activity such as campus drives or walk-ins.
    public class RecruitmentEvent
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [MaxLength(50)]
        public string EventType { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string EventName { get; set; } = string.Empty;

        [Required]
        public DateTime EventDate { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public AppUser? CreatedByUser { get; set; }
    }
}
