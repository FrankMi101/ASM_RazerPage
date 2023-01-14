using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;

namespace ASMWeb.Utility
{
    public class AuthenticationByAD
    {
        private readonly ILogger<LoginModel> _logger;

        public AuthenticationByAD( ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public static  bool AuthenValid(string username, string pwd)
        {
            string domain = "CEC";
            string _path = "";
            var domainAndUsername = domain + @"\" + username;
            var entry = new DirectoryEntry(_path, domainAndUsername, pwd);

            object isValidPassword = null;
            try
            {
                // authenticate (check password)
                isValidPassword = entry.NativeObject;
                return true;
            }
            catch (Exception ex)
            {
               ///  _logger.Log.Debug($"LDAP Authentication Failed for {domainAndUsername}");
                return false;
            }
        }

        public static bool ValidateCredentials(string userName, string password)
        {
            try
            {
                string domain = "CEC";
                using (var adContext = new PrincipalContext(ContextType.Domain, domain))
                {
                    return adContext.ValidateCredentials(userName, password);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
