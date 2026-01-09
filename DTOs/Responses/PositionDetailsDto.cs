namespace recruitment_process_portal_server.DTOs.Responses;

 
 public class PositionDetailsDto
{
    public int PositionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CurrentStatus { get; set; } = string.Empty;
    public int TotalApplications { get; set; }
    public int ShortlistedApplications { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
