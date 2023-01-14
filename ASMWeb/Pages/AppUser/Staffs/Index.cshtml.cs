
using ASMWeb.DataAccess;
using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASMWeb.Pages.AppUser.Staffs
{
    public class IndexModel : PageModel
    {
        private readonly IAppActionsCatalog _action; // IAppsRepository _db;
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<SelectListItem> SchoolList { get; set; }

        public IndexModel(IAppActionsCatalog action)
        {
            _action = action;
            SchoolList = CommonList.SchoolList(_action);
        }

        public void OnGet()
        {
            Employees = _action.Employees.GetAll(e => e.UnitID == "0529");// .GetAll(); // .ASM_Apps;

        }
    }
}
