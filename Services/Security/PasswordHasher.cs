
namespace recruitment_process_portal_server.Services.Security;

public static class PasswordHasher
{
    private const int WorkFactor = 11;

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: WorkFactor);
    }

    public static string Hash(string password) => HashPassword(password);

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
