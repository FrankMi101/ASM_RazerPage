// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Authen;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using ASMWeb.Utility;
using ASM.Models;

namespace ASMWeb.Areas.Identity.Pages.Account
{
    public class LoginModel2 : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IOptions<AppSettings> _appSettings;
        public LoginModel2(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _logger = logger;
            _appSettings = appSettings;
            _userManager = userManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            public string UserId { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

           // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                if ((!string.IsNullOrEmpty(Input.UserId)) && (!string.IsNullOrEmpty(Input.Password)))
                {

                     AuthenticationAD auth = new AuthenticationAD(_appSettings);
                    var authResult = auth.Authentication("CEC", Input.UserId, Input.Password);

                    if (authResult == "true")
                    {
                         var result = await _signInManager.PasswordSignInAsync("mif@tcdsb.org", "Frank123*", Input.RememberMe, lockoutOnFailure: false);
                    


                        CreateClaim(Input.UserId, SD.AdminRole, "Frank Mi");


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

        private async  void  CreateClaim(string userId, string userRole, string userName)
        {
            var claims = new List<Claim>
            {
                new Claim( ClaimTypes.Name, userId),
                new Claim( "FullName", userName),
                new Claim(ClaimTypes.Role, userRole),
            };

         
            var identity = new ClaimsIdentity(claims, "ASMCookieAuthentication");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("ASMCookieAuthentication", claimsPrincipal);
           
            User.AddIdentity(identity);
           


         //   var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

          //  User.AddIdentity(claimsIdentity);

            //var authProperties = new AuthenticationProperties
            //{
            //    AllowRefresh = true,
            //    // Refreshing the authentication session should be allowed.

            //    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30), // ExpiresUtc = DateTime.Now.AddMinutes(30),
            //    // The time at which the authentication ticket expires. A 
            //    // value set here overrides the ExpireTimeSpan option of 
            //    // CookieAuthenticationOptions set with AddCookie.

            //    IsPersistent = true,
            //    // Whether the authentication session is persisted across 
            //    // multiple requests. When used with cookies, controls
            //    // whether the cookie's lifetime is absolute (matching the
            //    // lifetime of the authentication ticket) or session-based.

            //    //IssuedUtc = <DateTimeOffset>,
            //    // The time at which the authentication ticket was issued.

            //    //RedirectUri = <string>
            //    // The full path or absolute URI to be used as an http 
            //    // redirect response value.
            //};
    
            //   await  HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            //                new ClaimsPrincipal(claimsIdentity), authProperties);
       
            // return RedirectToAction("Index", "Home");

        }
    }
}
