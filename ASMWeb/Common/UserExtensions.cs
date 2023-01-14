using System.Security.Claims;

namespace ASMWeb.Common
{
    public static class UserExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (user != null)   
                 return user.FindFirst(ClaimTypes.NameIdentifier).Value; 
            else 
                return null;
        }
    }
}
