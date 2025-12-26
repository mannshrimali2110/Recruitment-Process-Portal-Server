using System.ComponentModel.DataAnnotations;

namespace recruitment_process_portal_server.Models
{
     
    /// Represents an offer issued to a candidate for a job position.
    public class OfferLetter
    {
        [Key]
        public int OfferID { get; set; }

        [Required]
        public int LinkID { get; set; } // FK - JobCandidateLink

        [Required]
        public DateTime OfferDate { get; set; }

        public DateTime? JoiningDate { get; set; }

        [Required]
        public decimal CTC { get; set; }

        [Required]
        [MaxLength(50)]
        public string OfferStatus { get; set; } = "SENT"; // SENT, ACCEPTED, REJECTED

        // Navigation
        public JobCandidateLink JobCandidateLink { get; set; } = null!;
    }
}
