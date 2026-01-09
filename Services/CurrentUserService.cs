using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace recruitment_process_portal_server.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public int UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                throw new UnauthorizedAccessException("UserId claim missing.");

            return int.Parse(userIdClaim.Value);
        }
    }

    public string Role
    {
        get
        {
            var roleClaim = _httpContextAccessor.HttpContext?
                .User?
                .FindFirst(ClaimTypes.Role);

            if (roleClaim == null)
                throw new UnauthorizedAccessException("Role claim missing.");

            return roleClaim.Value;
        }
    }
}
