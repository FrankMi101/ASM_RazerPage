using ASM.DataAccess;
using ASM.Models;
using ASM.DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace ASM.Pages.Admin.AppsOperate
{
    
    [Authorize(Roles = $"{AppConstant.AdminRole}")]
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppServices _action; // IAppsRepository _db;
        public IEnumerable<Apps> AppsOperate { get; set; }
        public IEnumerable<SelectListItem> DeveloperList { get; set; }

        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly SignInManager<IdentityUser> _signInManager; 
       

        public IndexModel(IAppServices action, IHttpContextAccessor httpContextAccessor)
        {
            _action = action;
            DeveloperList =  CommonList.DeveloperList(_action);
            _httpContextAccessor = httpContextAccessor; 
            //_signInManager = signInManager; 
            //_userManager = userManager;

        }
       
        public void OnGet()
        {
            //  var para = new { Operate = "List", Type = "Name" };
            //  List<Apps> myist = _action.Apps.ListOfT("Read", para); 
            var usename = HttpContext.User.Identity.Name;
            var usname = _httpContextAccessor.HttpContext.User.Identity.Name;

            AppsOperate = _action.Apps.GetAll();// .GetAll(); // .ASM_Apps;

            //var ok = _signInManager.IsSignedIn(User);
            //var username = _userManager.GetUserName(User);

        }
}
}
