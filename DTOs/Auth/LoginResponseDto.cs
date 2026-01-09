namespace recruitment_process_portal_server.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}
