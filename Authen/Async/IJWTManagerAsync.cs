
namespace Authen
{
    public interface IJWTManagerAsync
    {
        Task<string> CreateTokenAsync(AuthUser authuser);
        Task<string> GenerateTokenAsync(string userName, string userRole, int expireMinutes = 30);
        Task<bool> ValidateTokenAsync(HttpRequestMessage Request);
    }
}