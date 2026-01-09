namespace recruitment_process_portal_server.DTOs.Responses;

 
 public class PositionStatusResponseDto
{
    public int PositionId { get; set; }
    public string PositionTitle { get; set; } = string.Empty;
    public string PreviousStatus { get; set; } = string.Empty;
    public string CurrentStatus { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public DateTime ChangedAt { get; set; }
    public int ChangedByUserId { get; set; }
}
