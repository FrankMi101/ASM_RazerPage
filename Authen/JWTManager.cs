using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Authen
{
    public class JWTManager : IJWTManager
    {
        // private const string secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";


        private readonly AppConfigJwtSettings _jwtSettings;

        private readonly IAppServices _action;
        private readonly string _dbIndicator = "Live";
        public JWTManager(IOptions<AppConfigJwtSettings> jwtSettings, IAppServices action)
        {
            _jwtSettings = jwtSettings.Value;
            _action = action;
        }
       
        public string GenerateToken(string userName, string userRole, int expireMinutes = 30)
        {

            var _signingCredentials = GetSigningCredentials();

            var now = DateTime.UtcNow;
            var _expires = now.AddMinutes(Convert.ToInt32(expireMinutes));
            var _issuer = "www.tcdsb.org/ASM";
            var _subject = new ClaimsIdentity(new[] { new Claim("UserName", userName),
                                                      new Claim("UserRole", userRole)});

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = _subject,
                Issuer = _issuer,
                Expires = _expires,
                SigningCredentials = _signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public string CreateToken(AuthUser authuser)
        {
            // string secretKey = _appSettings.JWTSecret;
            // var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            // var signingCredentials =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var signingCredentials = GetSigningCredentials();

            var claimsForToken = GetClaimsForToken(authuser);


            var jwtSecurityToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(2),
                signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }


        public List<Claim> GetClaimsForToken(AuthUser authUser)
        {
            //var userprofile = new UserProfileM(_action, authUser);
            //var obj = userprofile.GetUserProfile();

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("firstname", authUser.FirstName));
            claimsForToken.Add(new Claim("lastname", authUser.LastName));
            claimsForToken.Add(new Claim("unitid", authUser.UnitID));
           // claimsForToken.Add(new Claim("email", obj.Email));
            claimsForToken.Add(new Claim(ClaimTypes.Name, authUser.UserID.ToString()));  
            claimsForToken.Add(new Claim(ClaimTypes.Role, authUser.AppRole));

            return claimsForToken;
        }

        public bool ValidateToken(HttpRequestMessage Request)
        {
            try
            {
                if (Request.Headers.Authorization == null) return false;
                var JwtToken = Request.Headers.Authorization.Parameter;

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(JwtToken, validationParameters, out validatedToken);
                if (principal.Identity.IsAuthenticated) return true;
                return false;

            }
            catch (Exception)
            {

                return false;
            }
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = _jwtSettings.Issuer, //  "www.tcdsb.org",
                ValidAudience = _jwtSettings.Audience, //  "Sample",
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }
        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            try
            {
            string secretKey = _jwtSettings.JWTSecret;
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            return securityKey;

            }
            catch (Exception)
            {

                return null;
            }


        }
        private SigningCredentials GetSigningCredentials()
        {
            try
            {
            SymmetricSecurityKey securityKey = GetSymmetricSecurityKey();
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            return signingCredentials;

            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<string> GenerateTokenAsync(string userName, string userRole, int expireMinutes = 30)
        {

            var _signingCredentials = await GetSigningCredentialsAsync();

            var now = DateTime.UtcNow;
            var _expires = now.AddMinutes(Convert.ToInt32(expireMinutes));
            var _issuer = "www.tcdsb.org/ASM";
            var _subject = new ClaimsIdentity(new[] { new Claim("UserName", userName),
                                                      new Claim("UserRole", userRole)});

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = _subject,
                Issuer = _issuer,
                Expires = _expires,
                SigningCredentials = _signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public async Task<string> CreateTokenAsync(AuthUser authuser)
        {
            // string secretKey = _appSettings.JWTSecret;
            // var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            // var signingCredentials =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var signingCredentials = await GetSigningCredentialsAsync();

            var claimsForToken = await GetClaimsForTokenAsync(authuser);


            var jwtSecurityToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(2),
                signingCredentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }


        public async Task<List<Claim>> GetClaimsForTokenAsync(AuthUser authUser)
        {
            var userprofile = new UserProfileM(_action, authUser);
            var obj = await userprofile.GetUserProfileAsync();

            var claimsForToken = new List<Claim>();
            // claimsForToken.Add(new Claim("sub", authUser.UserID.ToString()));
            // claimsForToken.Add(new Claim("role", obj.RoleID.ToString()));
            claimsForToken.Add(new Claim("Firstname", obj.FirstName));
            claimsForToken.Add(new Claim("Lastname", obj.LastName));
            claimsForToken.Add(new Claim("Unitid", obj.UnitID));
            claimsForToken.Add(new Claim("Email", obj.Email));
            claimsForToken.Add(new Claim(ClaimTypes.Name, authUser.UserID.ToString())); // for  HttpContext.User.Identity.Name;
            claimsForToken.Add(new Claim(ClaimTypes.Role, obj.RoleID.ToString()));

            return claimsForToken;
        }

        public async Task<bool> ValidateTokenAsync(HttpRequestMessage Request)
        {
            try
            {
                if (Request.Headers.Authorization == null) return false;
                var JwtToken = Request.Headers.Authorization.Parameter;

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = await GetValidationParametersAsync();

                SecurityToken validatedToken;
                IPrincipal principal = tokenHandler.ValidateToken(JwtToken, validationParameters, out validatedToken);
                if (principal.Identity.IsAuthenticated) return true;
                return false;

            }
            catch (Exception)
            {

                return false;
            }
        }

        private async Task<TokenValidationParameters> GetValidationParametersAsync()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = _jwtSettings.Issuer, //  "www.tcdsb.org",
                ValidAudience = _jwtSettings.Audience, //  "Sample",
                IssuerSigningKey = await GetSymmetricSecurityKeyAsync()
            };
        }
        private async Task<SymmetricSecurityKey> GetSymmetricSecurityKeyAsync()
        {
            string secretKey = _jwtSettings.JWTSecret;
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            return securityKey;

        }
        private async Task<SigningCredentials> GetSigningCredentialsAsync()
        {
            SymmetricSecurityKey securityKey = await GetSymmetricSecurityKeyAsync();
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            return signingCredentials;
        }
    }
}
