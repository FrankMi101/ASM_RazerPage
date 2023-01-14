using ASM.DataAccess;
using ASM.Models;
using ASM.DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ASM.Pages.Admin.AppsOperate
{
    [Authorize]
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IAppServices _action;
        public Apps App { get; set; }
        public IEnumerable<SelectListItem> DeveloperList { get; set; }

        public EditModel(IAppServices action)
        {
            _action = action;
            DeveloperList = CommonList.DeveloperList(_action);
        }

        public void OnGet(int id)
        {
            // ********* Get User Information ******************************************
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var loginUserID = claim.Value;
            // *********************************************************************

            App = _action.Apps.GetById(id);//  _db.ASM_Apps.Find(id);
    //        App = _db.ASM_Apps.FirstOrDefault( u =>u.Id == id);
    //        App = _db.ASM_Apps.SingleOrDefault(u => u.Id == id);
    //        App = _db.ASM_Apps.Where(u => u.Id == id).FirstOrDefault();
        }
        public async Task<IActionResult> OnPost()
        {
           // ****** Customer Validation *****************************
            if (App.AppName == App.AppID)
            {
                ModelState.AddModelError("App.AppName","App ID and App Name Cannot exactly match App Name "); 
            }
            //********************************************************

            // ********* Get User Information ******************************************
           // var claimsIdentity = (ClaimsIdentity) User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var loginUserID = claim.Value;
            // *********************************************************************

            if (ModelState.IsValid) // Required server side validation
            {
                _action.Apps.Update(App);//  _db.ASM_Apps.Update(App);
              //  _action.Save(); // _db.Save();// await _db.SaveChangesAsync();
                TempData["success"] = "Application Update successfully";
                return RedirectToPage("Index");
           }
            return Page();
        }
    }
}
