namespace recruitment_process_portal_server.DTOs.Requests;

public class UpdateApplicationStatusRequest
{
    public int NewStatusId { get; set; }
    public string? Reason { get; set; }
}
