using System.ComponentModel.DataAnnotations;

namespace recruitment_process_portal_server.Models
{
     
    /// Central repository of skills / technologies
    /// (e.g., C#, SQL, React, Leadership)
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }

        [Required]
        [MaxLength(100)]
        public string SkillName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
