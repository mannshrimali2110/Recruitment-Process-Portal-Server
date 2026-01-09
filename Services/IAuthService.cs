using recruitment_process_portal_server.DTOs.Auth;

namespace recruitment_process_portal_server.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequestDto request);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}