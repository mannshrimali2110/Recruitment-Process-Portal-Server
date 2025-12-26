using System.ComponentModel.DataAnnotations;

namespace recruitment_process_portal_server.Models
{

    public class ApplicationStatus
    {
        [Key]
        public int StatusID { get; set; }

        [Required]
        [MaxLength(50)]
        public string StatusName { get; set; } = string.Empty;
    }
}
