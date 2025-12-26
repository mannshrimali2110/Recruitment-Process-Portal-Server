using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
     
    /// Junction table linking Candidates to Skills
    /// Includes experience and verification details.
    public class CandidateSkill
    {
        // Composite Key (configured via Fluent API)
        public int CandidateID { get; set; }
        public int SkillID { get; set; }

        public int? YearsOfExperience { get; set; }

        public bool IsVerified { get; set; } = false;

        public int? VerifiedByUserID { get; set; }

        public CandidateProfile Candidate { get; set; } = null!;
        public Skill Skill { get; set; } = null!;
        public AppUser? VerifiedByUser { get; set; }
    }
}
