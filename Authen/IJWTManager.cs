
namespace Authen
{
    public interface IJWTManager
    {
        string CreateToken(AuthUser authuser);
        string GenerateToken(string userName, string userRole, int expireMinutes = 30);
        bool ValidateToken(HttpRequestMessage Request);

        Task<string> CreateTokenAsync(AuthUser authuser);
        Task<string> GenerateTokenAsync(string userName, string userRole, int expireMinutes = 30);
        Task<bool> ValidateTokenAsync(HttpRequestMessage Request);

    }
}