using System.ComponentModel.DataAnnotations;

namespace recruitment_process_portal_server.Models
{
     
    /// Lookup table for document verification status.
    public class DocVerifyStatus
    {
        [Key]
        public int StatusID { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; } = string.Empty; // Pending, Verified, Rejected
    }
}
