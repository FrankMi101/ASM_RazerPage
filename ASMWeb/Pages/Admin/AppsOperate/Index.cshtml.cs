
using ASMWeb.DataAccess;
using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using ASMWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASMWeb.Pages.Admin.AppsOperate
{
    [Authorize(Roles = $"{SD.AdminRole},{SD.PrincipalRole}")]
     public class IndexModel : PageModel
    {
        private readonly IAppActionsCatalog _action; // IAppsRepository _db;
        public IEnumerable<Apps> AppsOperate { get; set; }
        public IEnumerable<SelectListItem> DeveloperList { get; set; }
        public IndexModel(IAppActionsCatalog action)
        {
            _action = action;
            DeveloperList =  CommonList.DeveloperList(_action);

        }
       
        public void OnGet()
        {
          //  var para = new { Operate = "List", Type = "Name" };
          //  List<Apps> myist = _action.Apps.ListOfT("Read", para); 

            AppsOperate = _action.Apps.GetAll();// .GetAll(); // .ASM_Apps;

    }
}
}
