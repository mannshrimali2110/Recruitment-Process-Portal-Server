using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
    public class JobCandidateLink
    {
        [Key]
        public int LinkID { get; set; }

        public int PositionID { get; set; }
        public int CandidateID { get; set; }
        public int CurrentStatusID { get; set; }
        public int? LinkedByUserID { get; set; }
        public DateTime LinkDate { get; set; }

        public CandidateProfile CandidateProfile { get; set; } = null!;
        public JobPosition JobPosition { get; set; } = null!;
        public ApplicationStatus ApplicationStatus { get; set; } = null!;
        public AppUser? LinkedByUser { get; set; }

        // CHILD COLLECTIONS (THIS WAS MISSING)
        public ICollection<InterviewSchedule> InterviewSchedules { get; set; } = new List<InterviewSchedule>();
        public ICollection<ScreeningFeedback> ScreeningFeedbacks { get; set; } = new List<ScreeningFeedback>();
        public ICollection<OfferLetter> OfferLetters { get; set; } = new List<OfferLetter>();
        public ICollection<StatusChangeLog> StatusChangeLogs { get; set; } = new List<StatusChangeLog>();
    }
}
