#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM.DataAccess;
using ASM.Models;
using ASM.DataAccess.Repository;

namespace ASM.Pages.Admin.AppsModel
{
    public class EditModel : PageModel
    {
         [BindProperty]
        public AppsModels Models { get; set; }
        public IEnumerable<SelectListItem> DeveloperList { get; set; }
        public IEnumerable<SelectListItem> GrantTypeList { get; set; }
        private readonly IAppServices _action;

        public EditModel(IAppServices action)
        {
            _action = action;
            DeveloperList = CommonList.DeveloperList(_action);
            GrantTypeList = CommonList.GrantTypeList(_action);
        }

 
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           // _action.Attach(Models).State = EntityState.Modified;

            try
            {
                 _action.AppModels.Update(Models);// await _action.SaveChangesAsync();
              //  _action.Save();
                TempData["success"] = "Application Model Save successfully";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelsExists(Models.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new {appId= Models.AppID });
        }

        private bool ModelsExists(int id)
        {
            Models = _action.AppModels.GetById(id);
            if (Models != null) return true;
            return false;

           // return _action.ASM_AppModels.Any(e => e.Id == id);
        }
    }
}
