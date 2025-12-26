using System.ComponentModel.DataAnnotations;

namespace recruitment_process_portal_server.Models
{
    public class InterviewType
    {
        [Key]
        public int TypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeName { get; set; } = string.Empty;
    }
}
