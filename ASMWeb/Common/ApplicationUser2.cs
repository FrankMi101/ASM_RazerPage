
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ASMWeb.Common
{
    public class ApplicationUser2: IdentityUser
    {
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        //{
        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, this.UserId));
        //    return userIdentity;
        //}
    }
}
