namespace recruitment_process_portal_server.DTOs.Requests;

public class ScheduleInterviewRequest
{
    public int InterviewTypeId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? MeetingLink { get; set; }
}
