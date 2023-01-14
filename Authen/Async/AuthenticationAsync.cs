using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Configuration;
using System.DirectoryServices.Protocols;
using System.Net;
using Microsoft.Extensions.Options;
using ASM.Models;

namespace Authen
{
    public class AuthenticationAsync : IAuthenticationAsync
    {
        private readonly AppSettings _appSettings;
        private readonly string _ldapPath;
        private readonly string _domain;
        public AuthenticationAsync(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _ldapPath = _appSettings.LdapPath.ToString();
            _domain = _appSettings.Domain;

        }
        public async Task<bool>  IsAuthenticatedAsync(string _domain, string username, string pwd)
        {
            if (AuthenticateMethod() == "NameOnly") return true;
            if ( await  AuthenticateResultAsync(username, pwd) == "true") return  true;
            return false;
        }
        public async Task<bool> IsAuthenticatedAsync(AuthUser authUser)
        {
            if (authUser.AuthMethod == "Settings") authUser.AuthMethod = AuthenticateMethod();

            if (authUser.AuthMethod == "NameOnly") return true;

            if (await AuthenticateResultAsync(authUser.UserID, authUser.Password) == "true") return true;
            return false;
        }

        public async Task<string> AuthenticatedAsync(string domain, string username, string pwd)
        {
            return await AuthenticateResultAsync(username, pwd);
        }
        private async Task<string> AuthenticateResultAsync(string username, string pwd)
        {
            try
            {
                // string domainAndUsername = _domain + "\\" + username;
                DirectoryEntry entry = new DirectoryEntry(_ldapPath, username, pwd);
                try
                {
                    Object obj = entry.NativeObject; //  .NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);
                    search.Filter = "(SAMAccountName=" + username + ")";
                    search.PropertiesToLoad.Add("cn");
                    SearchResult result = search.FindOne();

                    if (result == null)
                        return "Login Failed with Name or Password Error";
                    else
                        return "true";
                }
                catch (Exception ex)
                {
                    return "Login Failed at NativeObject Authentication- " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                return "Login Failed at DirectoryEntry Authentication- " + ex.Message;
            }

        }


        private string AuthenticateMethod()
        {

            string authMethod = _appSettings.AuthMethod;

            if (authMethod == "NameOnly")
            {
                string hostName = System.Net.Dns.GetHostName();
                string appServers = _appSettings.AppServer;
                if (appServers.Contains(hostName)) authMethod = "NameOnlyFalse";
            }
            return authMethod;
        }

        private string AuthenticateMethod(string loginUser)
        {
            string authMethod = AuthenticateMethod();
            string developers = _appSettings.Developers;
            if (developers.Contains(loginUser.ToLower())) authMethod = "NameOnly";
            return authMethod;
        }

        public static string GetDomain()
        {
            return System.Environment.UserDomainName;
        }
        public static string GetDomain(string objName)
        {
            if (objName == "") return GetDomain();
            int stop = objName.IndexOf("\\");
            return (stop > -1) ? objName.Substring(0, stop) : string.Empty;
        }
        public static string GetUserName()
        {
            return System.Environment.UserName;
        }
        public static string GetUserName(string objName)
        {
            if (objName == "") return GetUserName();
            int stop = objName.IndexOf("\\");
            return (stop > -1) ? objName.Substring(stop + 1, objName.Length - stop - 1) : string.Empty;
        }

        public static string AuthResult(string username, string password, string domain)
        {
            try
            {
                LdapConnection ldapConnection = new LdapConnection("host:port");

                // authenticate the username and password
                using (ldapConnection)
                {
                    // pass in the network creds, and the domain.
                    var networkCredential = new NetworkCredential(username, password, domain);

                    // if we're using unsecured port 389, set to false. If using port 636, set this to true.
                    ldapConnection.SessionOptions.SecureSocketLayer = false;

                    // since this is an internal application, just accept the certificate either way
                    ldapConnection.SessionOptions.VerifyServerCertificate += delegate { return true; };

                    // to force NTLM\Kerberos use AuthType.Negotiate, for non-TLS and unsecured, just use AuthType.Basic
                    ldapConnection.AuthType = AuthType.Basic;

                    // authenticate the user
                    ldapConnection.Bind(networkCredential);

                    return "Successfull";
                }
            }
            catch (LdapException ldapException)
            {
                //Authentication failed, exception will dictate why
                return ldapException.Message;
            }
        }
    }

}
