using System.ComponentModel.DataAnnotations.Schema;

namespace recruitment_process_portal_server.Models
{
     
    /// Defines required and preferred skills for a job position.
    /// Junction table between JobPosition and Skill.
    public class JobSkillDefinition
    {
        public int PositionID { get; set; }

        [ForeignKey(nameof(PositionID))]
        public JobPosition? JobPosition { get; set; }

        public int SkillID { get; set; }

        [ForeignKey(nameof(SkillID))]
        public Skill? Skill { get; set; }

        public bool IsMinimum { get; set; } = true;
    }
}
