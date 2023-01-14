namespace Authen
{
    public interface IAuthentication
    {
        string Authenticated(string domain, string username, string pwd);
        bool IsAuthenticated(AuthUser authUser);
        bool IsAuthenticated(string _domain, string username, string pwd);

        Task<string> AuthenticatedAsync(string domain, string username, string pwd);
        Task<bool> IsAuthenticatedAsync(AuthUser authUser);
        Task<bool> IsAuthenticatedAsync(string _domain, string username, string pwd);
    }
}