using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authen
{
    public class AuthService : IAuthService
    {
        public IAuthentication Authencate { get; private set; }
        public IJWTManager JwtManagers { get; private set; }

        public AuthService(IOptions<AppConfigJwtSettings> jwtSettings, IOptions<AppConfigAppSettings> appSettings, IAppServices action)
        {
            Authencate = new Authentication(appSettings);
            JwtManagers = new JWTManager(jwtSettings, action);
        }
   

        public Task<bool> AuthenticateAsync(AuthUser authUser)
        {
           return Authencate.IsAuthenticatedAsync(authUser);
        } 
        public void Dispose()
        {
        }
    }
}
