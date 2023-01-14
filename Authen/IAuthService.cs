using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authen
{
    public interface IAuthService :IDisposable
    {
        IAuthentication Authencate { get; }
        IJWTManager  JwtManagers { get; }
        //IAuthenticationAsync AuthencateAsync { get; }
        //IJWTManagerAsync JwtManagersAsync { get; }
        Task<bool> AuthenticateAsync(AuthUser authUser);
    }
}
