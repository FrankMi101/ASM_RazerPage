using ASM.DataAccess;
using ASM.Models;
using ASM.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM.Pages.Admin.AppsOperate
{
    [BindProperties]
    public class CreateModel : PageModel
    {
       // private readonly IAppsRepository _db;
        private readonly IAppServices _action;
        public IEnumerable<SelectListItem> DeveloperList { get; set; }

        [BindProperty]
        public Apps App { get; set; }

        public CreateModel(IAppServices action)
        {
            //_db = DBNull;
            _action = action;
            DeveloperList = CommonList.DeveloperList(_action);
        }

        public void OnGet()
        {
            ViewData["Developers"] = CommonList.DeveloperList(_action);

        }
        public async Task<IActionResult> OnPost()
        {
           // Customer Validation
           // *********************************************************
            if (App.AppName == App.AppID)
            {
                ModelState.AddModelError(String.Empty,"App ID and App Name Cannot exactly match App Name "); 
            }
            //********************************************************
            if (ModelState.IsValid) // Required server side validation
            {

                _action.Apps.Add(App);//  _db.Add(App);//  await _db.ASM_Apps.AddAsync(App);
                _action.Save();//  _db.Save(); //  await _db.SaveChangesAsync();

                TempData["success"] = "Application added successfully";
            return RedirectToPage("Index");
           }
            return Page();
        }
    }
}
