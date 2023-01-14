
using ASM.DataAccess;
using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM.Pages.AppUser.Staffs
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServices _action; // IAppsRepository _db;
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<SelectListItem> SchoolList { get; set; }

        public IndexModel(IAppServices action)
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
