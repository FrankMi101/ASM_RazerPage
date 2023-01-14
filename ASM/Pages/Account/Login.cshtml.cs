using Authen;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using ASM.DataAccess.Repository;
using ASM.Models;
using ASM.DataAccess.Services;
using Newtonsoft.Json;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace ASM.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IOptions<AppConfigAppSettings> _appSettings;
        private readonly IAppServices _action;
        private readonly IAuthService _auth;
        public LoginModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, IOptions<AppConfigAppSettings> appSettings, IAppServices action, IAuthService auth)
        {
            _signInManager = signInManager;
            _logger = logger;
            _appSettings = appSettings;
            _userManager = userManager;
            _action = action;
            _auth = auth;
        }

        public void OnGet()
        {
            this.Credential = new Credential { UserName = "mif", Password = "", RememberMe = true };
        }
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                if ((!string.IsNullOrEmpty(Credential.UserName)) && (!string.IsNullOrEmpty(Credential.Password)))
                {

                    //   IAuthentication auth = new Authentication(_appSettings);
                    //  auth.Authenticated("CEC", Credential.UserName, Credential.Password);

                    var authUser = new AuthUser()
                    {
                        UserID = Credential.UserName,
                        Password = Credential.Password,
                        AuthMethod = "Settings"
                    };

                    var authResult = await _auth.AuthenticateAsync(authUser);

                    if (authResult)
                    {                        
                        await CreateClaim();

                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                }
                return Page();
            }
            return Page();
        }

        private async Task CreateClaim()
        {
            try
            {
                var para = new { Operate = "GetProfile", UserID = Credential.UserName };
                var userProfile = _action.UserProfile.ObjectOfT("Read", para);


                var userId = Credential.UserName;
                var userRole = userProfile.RoleID;
                var userName = userProfile.FirstName + " " + userProfile.LastName;


                var authUser = new AuthUser()
                {
                    AppRole = userProfile.RoleID,
                    AppID = AppConstant.AppName,
                    UserID = Credential.UserName,
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName,
                    UnitID = userProfile.UnitID
                };


                var claims = new List<Claim>
            {
                new Claim( "FullName", userName),
                new Claim( "AppRole", userRole),
                new Claim(ClaimTypes.Name,userId),
                new Claim(ClaimTypes.Role,userRole) ,
                new Claim("UnitID", userProfile.UnitID)
            };

                //var identity = new ClaimsIdentity(claims, "ASMCookieAuthentication");
                //ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                //await HttpContext.SignInAsync("ASMCookieAuthentication", claimsPrincipal);

                var identity = new ClaimsIdentity(claims, ASM.AppConstant.AuthCookName);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    // Refreshing the authentication session should be allowed.
                    // ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5), // ExpiresUtc = DateTime.Now.AddMinutes(30),
                    IsPersistent = Credential.RememberMe,
                };

                // Role Manage
                //    var user = new AppsUser { UserName = userName, UserID = userId, RoleID = userRole,FirstName ="", LastName ="", UnitID ="" };
                //     await _userManager.AddToRoleAsync(user, userRole);


                //  await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

                await SetAppToken(authUser);

            
            }
            catch (Exception)
            {
                throw new Exception("Identify User Error");
            }
        }

        private async Task  SetAppToken(AuthUser authUser)
        {
            try
            {
                var token = await  _auth.JwtManagers.CreateTokenAsync(authUser);

                if (token != null)
                {
                    HttpContext.Session.SetString("access_token", token); 
                }
            }
            catch (Exception)
            {

                HttpContext.Session.SetString("access_token", "");
            }



            //if (resp != null && resp.IsSuccess) {

            //    string token =   JsonConvert.DeserializeObject<string>(Convert.ToString(resp.Result));
            //    HttpContext.Session.SetString("acces_token", token);
            //}
        }

    }

}
