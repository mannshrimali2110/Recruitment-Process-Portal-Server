namespace recruitment_process_portal_server.DTOs.Requests;

public class ConvertToEmployeeRequest
{
    public DateTime JoiningDate { get; set; }
    public string WorkEmail { get; set; } = string.Empty;
}
