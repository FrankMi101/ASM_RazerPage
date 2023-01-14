using ASMWeb.DataAccess;
using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASMWeb.Pages.Admin.Schools
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IAppActionsCatalog _action;
        public School School { get; set; }
        public IEnumerable<SelectListItem> AreaList { get; set; }
        public IEnumerable<SelectListItem> DistrictList { get; set; }
        public IEnumerable<SelectListItem> PanelList { get; set; }
        public IEnumerable<SelectListItem> PrincipalList { get; set; }

        public EditModel(IAppActionsCatalog action)
        {
            _action = action;
            AreaList = CommonList.AreaList(_action);
            DistrictList = CommonList.DistrictList(_action);
            PanelList = CommonList.PanelList(_action);
            PrincipalList = CommonList.PrincipalList(_action);
        }

        public void OnGet(int id)
        {
            School = _action.Schools.GetById(id);
         }
        public async Task<IActionResult> OnPost()
        {
           // Customer Validation
           
            if (ModelState.IsValid) // Required server side validation
            {
                /*   EF action
                _action.Schools.Update(School);//  _db.ASM_Apps.Update(App);
                _action.Save(); // _db.Save();// await _db.SaveChangesAsync();
                */
                // ********** Dapper SP ************************************************
                var school = new
                {
                    Operate = "Update",
                    School.Id,
                    School.UnitCode,
                    School.BSID,
                    School.UnitName,
                    School.BriefName,
                    School.Status,
                    School.PrincipalID,
                    School.AreaCode,
                    School.TypeCode,
                    School.DistrictCode
                };
                string result = _action.Schools.ValueOfT("Edit", school);
               // ******************************************************************
                if (result.Contains("Successfully"))
                {
                    TempData["success"] = "Application Update successfully";
                    return RedirectToPage("Index");
                }
                else
                {
                    TempData["error"] = "Application Update Failed";
                    return Page();
                }
            }
            return Page();
        }
    }
}
