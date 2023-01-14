namespace Authen
{
    public interface IAuthenticationAsync
    {
        Task<string> AuthenticatedAsync(string domain, string username, string pwd);
        Task<bool> IsAuthenticatedAsync(AuthUser authUser);
        Task<bool> IsAuthenticatedAsync(string _domain, string username, string pwd);
    }
}