using Microsoft.EntityFrameworkCore;
using recruitment_process_portal_server.Data;
using recruitment_process_portal_server.DTOs.Auth;
using recruitment_process_portal_server.Models;
using recruitment_process_portal_server.Services.Security;

namespace recruitment_process_portal_server.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly JwtTokenService _jwt;

    private static readonly HashSet<string> AllowedRoles =
        new() { "Recruiter", "HR", "Interviewer" };

    public AuthService(ApplicationDbContext context, JwtTokenService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        if (!AllowedRoles.Contains(request.RoleName))
            throw new InvalidOperationException("Invalid role.");

        if (await _context.AppUsers.AnyAsync(u => u.Email == request.Email))
            throw new InvalidOperationException("Email already exists.");

        var role = await _context.UserRoles
            .SingleOrDefaultAsync(r => r.RoleName == request.RoleName)
            ?? throw new InvalidOperationException("Role not found.");

        var user = new AppUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = PasswordHasher.HashPassword(request.Password),
            RoleID = role.RoleID,
            IsActive = true
        };

        _context.AppUsers.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.AppUsers
            .Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Email == request.Email && u.IsActive);

        if (user == null ||
            !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var token = _jwt.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(8)
        };
    }
}
