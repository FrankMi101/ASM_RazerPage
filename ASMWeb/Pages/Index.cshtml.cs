using ASMWeb.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ASMWeb.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string UserID { get; set; }
        public string UserName { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            UserID = User.GetUserId();
        }

       // public IActionResult OnGet()
       // {
       //    return RedirectToPage("/AppUser/Staffs/Index");
       //}
        public void OnGet()
        {
            // return RedirectToPage("/AppUser/Staffs/Index");
        }
    }
}