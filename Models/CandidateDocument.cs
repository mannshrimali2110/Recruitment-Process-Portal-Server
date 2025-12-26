using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
     
    /// Stores uploaded documents for candidates (IDs, certificates, etc.)
    public class CandidateDocument
    {
        [Key]
        public int DocumentID { get; set; }

        [Required]
        public int CandidateID { get; set; }

        [Required]
        [MaxLength(50)]
        public string DocType { get; set; } = string.Empty; // Aadhar, Degree, PAN, etc.

        [Required]
        public string FilePath { get; set; } = string.Empty;

        [Required]
        public int StatusID { get; set; } // FK → DocVerifyStatus

        public int? VerifiedByUserID { get; set; }

        // Navigation properties
        public CandidateProfile Candidate { get; set; } = null!;
        public DocVerifyStatus Status { get; set; } = null!;
        public AppUser? VerifiedByUser { get; set; }
    }
}
