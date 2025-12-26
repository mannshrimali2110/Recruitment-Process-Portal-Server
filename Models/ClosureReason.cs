using System.ComponentModel.DataAnnotations;

namespace recruitment_process_portal_server.Models
{
     
    /// Represents standardized reasons for closing a job position.
    public class ClosureReason
    {
        [Key]
        public int ReasonID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ReasonText { get; set; } = string.Empty;
    }
}
