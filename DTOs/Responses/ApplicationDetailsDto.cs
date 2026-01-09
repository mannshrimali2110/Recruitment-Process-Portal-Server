namespace recruitment_process_portal_server.DTOs;

public class ApplicationDetailsDto
{
    public int LinkId { get; set; }
    public string CandidateName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string CurrentStatus { get; set; } = string.Empty;
    public DateTime AppliedOn { get; set; }

    public IReadOnlyList<ApplicationStatusLogDto> StatusHistory { get; set; }
        = Array.Empty<ApplicationStatusLogDto>();
}
