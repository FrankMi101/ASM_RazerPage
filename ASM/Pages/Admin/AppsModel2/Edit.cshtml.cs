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
using ASM.DataAccess.Services;
using Newtonsoft.Json;

namespace ASM.Pages.Admin.AppsModel2
{
    public class EditModel : PageModel
    {
         [BindProperty]
        public AppsModels Models { get; set; }
        public IEnumerable<SelectListItem> DeveloperList { get; set; }
        public IEnumerable<SelectListItem> GrantTypeList { get; set; }
        private readonly IAppServices _action;
        private readonly IAppsModelServiceAPICall _appsModelService;
        public EditModel(IAppServices action, IAppsModelServiceAPICall appsModelService)
        {
            _action = action;
            _appsModelService = appsModelService;
            DeveloperList = CommonList.DeveloperList(_action);
            GrantTypeList = CommonList.GrantTypeList(_action);
            _appsModelService.SetVersion("v2");
        }

 
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Developers"] = CommonList.DeveloperList(_action);
            ViewData["GrantTypes"] = CommonList.GrantTypeList(_action);  //AccessMethodList;

            var respons = await _appsModelService.GetAsync<APIResponse>("IEP",id);
            if (respons != null && respons.IsSuccess)
            {
                Models = JsonConvert.DeserializeObject<AppsModels>(Convert.ToString(respons.Result));
            }

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

            try
            {

                var response = await _appsModelService.UpdateAsync<APIResponse>(Models);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Application Model added successfully";
                    return RedirectToPage("./Index", new { appId = Models.AppID });
                }
                return Page();
  
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
        }
    }
}
