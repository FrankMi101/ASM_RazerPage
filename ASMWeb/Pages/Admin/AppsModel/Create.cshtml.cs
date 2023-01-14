#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASMWeb.DataAccess;
using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASMWeb.Pages.Admin.AppsModel
{
    public class CreateModel : PageModel
    {
        private readonly IAppActionsCatalog _action;

        public CreateModel(IAppActionsCatalog action)
        {
            _action = action;
        }

        public IActionResult OnGet()
        {

            //  var DeveloperList  = CommonList.DeveloperList();
            //  var AccessMethodList = CommonList.AccessMethodList();

            //IEnumerable<SelectListItem> DeveloperList = _action.Developers.GetAll().Select(
            //    d => new SelectListItem
            //    {
            //        Text = d.Name,
            //        Value = d.UserID
            //    });

            //IEnumerable<SelectListItem> AccessMethodList = _action.GrantType.GetAll().Select(
            //   d => new SelectListItem
            //   {
            //       Text = d.AccessMethod,
            //       Value = d.AccessMethod
            //   });
            //// ViewBag.DeveloperList = DeveloperList;
            ViewData["Developers"] = CommonList.DeveloperList(_action);
            ViewData["GrantTypes"] = CommonList.GrantTypeList(_action);  //AccessMethodList;

            return Page();
        }

        [BindProperty]
        public AppsModels Models { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _action.AppModels.Add(Models); //  _action.ASM_AppModels.Add(Models);
            _action.Save(); // await _action.SaveChangesAsync();

            TempData["success"] = "Application Model added successfully";
            return RedirectToPage("./Index", new { appId = Models.AppID });
        }
    }
}
