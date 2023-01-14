using Authen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.CodeDom;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ASM.Pages.Account
{
    [Authorize]
    public class UserProfileModelModel : PageModel
    {
        private readonly UserManager<User> userManager;

        [BindProperty]
        public UserProfileViewModel UserProfile { get; set; }


        [BindProperty]
        public string SuccessMessage { get; set; }
        public UserProfileModelModel(UserManager<User> userManageer)
        {
            this.userManager = userManageer;
            this.UserProfile = new UserProfileViewModel();
            this.SuccessMessage= string.Empty;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            this.SuccessMessage = string.Empty;
            var (user, departmentClaim, positionClaim) = await GetUserInfoAsync();

            this.UserProfile.Department = departmentClaim?.Value;
            this.UserProfile.Position = positionClaim?.Value;
            this.UserProfile.Email = User.Identity.Name;

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                var (user, departmentClaim, positionClaim) = await GetUserInfoAsync();

                userManager.ReplaceClaimAsync(user, departmentClaim, new Claim(departmentClaim.Type, UserProfile.Department));
                userManager.ReplaceClaimAsync(user, positionClaim, new Claim(positionClaim.Type, UserProfile.Position));
                return Page();

            }
            catch (Exception)
            {
                ModelState.AddModelError("UserProfle", "Error occured when");
                
            }
            this.SuccessMessage = "User Profile is saved successfully";
            return Page();

        }

        private async Task<(User, Claim, Claim)> GetUserInfoAsync()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var claims = await userManager.GetClaimsAsync(user);
            var departmentClaim = claims.FirstOrDefault(x => x.Type == "Department");
            var positionClaim = claims.FirstOrDefault(x => x.Type == "Position");

            return (user, departmentClaim, positionClaim);
        }
    }

    public class UserProfileViewModel
    {
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Position { get; set; }
    }
}
