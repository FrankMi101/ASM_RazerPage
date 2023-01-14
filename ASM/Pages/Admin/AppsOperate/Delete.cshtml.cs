using ASM.DataAccess;
using ASM.Models;
using ASM.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM.Pages.Admin.AppsOperate
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IAppServices _action; // IAppsRepository _db;
        public Apps App { get; set; }
        public IEnumerable<SelectListItem> DeveloperList { get; set; }

        public DeleteModel(IAppServices action)
        {
            _action = action;
            DeveloperList = CommonList.DeveloperList(_action);
        }

        public void OnGet(int id)
        {
            App = _action.Apps.GetById(id);// _db.ASM_Apps.Find(id);
    //        App = _db.ASM_Apps.FirstOrDefault( u =>u.Id == id);
    //        App = _db.ASM_Apps.SingleOrDefault(u => u.Id == id);
    //        App = _db.ASM_Apps.Where(u => u.Id == id).FirstOrDefault();
        }
        public async Task<IActionResult> OnPost()
        {
            var appFromDb = _action.Apps.GetById(App.Id);// _db.GetById(App.Id);// _db.ASM_Apps.Find(App.Id); 
            if (appFromDb != null)
            {
                _action.Apps.Remove(appFromDb);//  _db.Remove(appFromDb); // _db.ASM_Apps.Remove(appFromDb);
                _action.Save(); //  _db.Save();// await _db.SaveChangesAsync();
                TempData["success"] = "Application deleted successfully";
                return RedirectToPage("Index");

            }
          return Page();
        }
    }
}
