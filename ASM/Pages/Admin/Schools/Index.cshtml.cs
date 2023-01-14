using ASM.DataAccess;
using ASM.Models;
using ASM.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ASM.Pages.Admin.Schools
{
    [Authorize(Roles = $"{AppConstant.AdminRole},{AppConstant.PrincipalRole}")]
    public class IndexModel : PageModel
    {
        private readonly IAppServices _action; // IAppsRepository _db;
        public IEnumerable<School> Schools { get; set; }
        public IEnumerable<SelectListItem> AreaList { get; set; }
        public IEnumerable<SelectListItem> DistrictList { get; set; }
        public IEnumerable<SelectListItem> PanelList { get; set; }
        public IEnumerable<SelectListItem> PrincipalList { get; set; }
        public IEnumerable<SelectListItem> SearchList { get; set; }
        public List<SelectListItem> SearchByList { get; set; }

        public string SearchBy { get; set; }
        public string SearchByValue { get; set; }
        public IndexModel(IAppServices action)
        {
            _action = action;
            AreaList = CommonList.AreaList(_action);
            DistrictList = CommonList.DistrictList(_action);
            PanelList = CommonList.PanelList(_action);
            PrincipalList = CommonList.PrincipalList(_action);
            SearchByList = CommonList.SearchByList();
            SearchList = PanelList; 
            

        }

        public void OnGet(string panel)
        {
            if (panel == "All")
                Schools = _action.Schools.GetAll();// .GetAll(); // .ASM_Apps;
            else
                Schools = _action.Schools.GetAll(s=>s.TypeCode == panel );

        }

        
        public   async Task<IActionResult> OnPostSearch( string searchby)
        {
            if (searchby == "Panel")
                SearchList = PanelList;
            else if (searchby == "Area")
                SearchList = AreaList;
            else
                SearchList = DistrictList;

            return Page();

        }
    }
}
