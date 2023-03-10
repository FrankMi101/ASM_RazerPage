#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASMWeb.DataAccess;
using ASMWeb.Models;
using ASMWeb.DataAccess.Repository;

namespace ASMWeb.Pages.Admin.AppsModel
{
    public class DetailsModel : PageModel
    {
        private readonly IAppActionsCatalog _action;

        public DetailsModel(IAppActionsCatalog action)
        {
            _action = action;
        }

        public AppsModels Models { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Developers"] = CommonList.DeveloperList(_action);
            ViewData["GrantTypes"] = CommonList.GrantTypeList(_action);  //AccessMethodList;


            Models = _action.AppModels.GetById(id); //await _action.ASM_AppModels.FirstOrDefaultAsync(m => m.Id == id);

            if (Models == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
