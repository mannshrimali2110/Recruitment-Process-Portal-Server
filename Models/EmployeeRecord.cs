using System.ComponentModel.DataAnnotations;

namespace recruitment_process_portal_server.Models
{
     
    /// Represents an employee created after a candidate successfully joins.
    public class EmployeeRecord
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        public int CandidateID { get; set; } // FK - CandidateProfile

        [Required]
        public int PositionID { get; set; } // FK - JobPosition

        [Required]
        public DateTime DateOfJoining { get; set; }

        [Required]
        [MaxLength(255)]
        public string WorkEmail { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string WorkStatus { get; set; } = "ACTIVE"; // ACTIVE, PROBATION, EXITED

        // Navigation
        public CandidateProfile Candidate { get; set; } = null!;
        public JobPosition JobPosition { get; set; } = null!;
    }
}
