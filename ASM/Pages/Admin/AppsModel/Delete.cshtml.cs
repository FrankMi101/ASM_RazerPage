#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASM.DataAccess;
using ASM.Models;
using ASM.DataAccess.Repository;

namespace ASM.Pages.Admin.AppsModel
{
    public class DeleteModel : PageModel
    {
        private readonly IAppServices _action;

        public DeleteModel(IAppServices action)
        {
            _action = action;
        }

        [BindProperty]
        public AppsModels Models { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Developers"] = CommonList.DeveloperList(_action);
            ViewData["GrantTypes"] = CommonList.GrantTypeList(_action);  //AccessMethodList;


            Models = _action.AppModels.GetById(id);//  await _action.ASM_AppModels.FirstOrDefaultAsync(m => m.Id == id);

            if (Models == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Models =   _action.AppModels.GetById(id);//   await _action.ASM_AppModels.FindAsync(id);

            if (Models != null)
            {
                _action.AppModels.Remove(Models);// _action.ASM_AppModels.Remove(Models);
                _action.Save();// await _action.SaveChangesAsync();
            }
            TempData["success"] = "Application Model Remove successfully";
            return RedirectToPage("./Index", new { appId = Models.AppID });
            
        }
    }
}
