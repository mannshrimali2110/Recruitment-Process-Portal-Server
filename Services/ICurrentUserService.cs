namespace recruitment_process_portal_server.Services;

public interface ICurrentUserService
{
    int UserId { get; }
    string Role { get; }
    bool IsAuthenticated { get; }
}
