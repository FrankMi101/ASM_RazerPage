using ASM.DataAccess;
using ASM.Models;
using ASM.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 


 
namespace ASM.Pages.Admin.AppsModel
{
    public class UpsertModel : PageModel
    {
        [BindProperty]
        public AppsModels Models { get; set; }
        public IEnumerable<SelectListItem> DeveloperList { get; set; }
        public IEnumerable<SelectListItem> GrantTypeList { get; set; }

        private readonly IAppServices _action;

        public UpsertModel(IAppServices action)
        {
            _action = action;
            Models = new();
            DeveloperList = CommonList.DeveloperList(_action);
            GrantTypeList = CommonList.GrantTypeList(_action);   
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
 
            Models = _action.AppModels.GetFirstOrDefault(u => u.Id == id);

            //if (Models == null)
            //{
            //    return NotFound();
            //}
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
         public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
 
            try
            {
                _action.AppModels.Update(Models);// await _action.SaveChangesAsync();
                _action.Save();
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

            return RedirectToPage("./Index", new { appId = Models.AppID });
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
