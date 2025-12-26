using System.ComponentModel.DataAnnotations;

namespace recruitment_process_portal_server.Models
{
    /// Represents how a candidate profile was acquired
    /// (CV Parse, Manual Entry, Bulk Upload, Referral, etc.)

    public class AcquisitionSource
    {
        [Key]
        public int SourceID { get; set; }

        [Required]
        [MaxLength(50)]
        public string SourceName { get; set; } = string.Empty;
    }
}
