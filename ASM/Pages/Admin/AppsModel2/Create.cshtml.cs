#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASM.DataAccess;
using ASM.DataAccess.Repository;
using ASM.DataAccess.Services;
using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;

namespace ASM.Pages.Admin.AppsModel2
{
    public class CreateModel : PageModel
    {
        private readonly IAppServices _action;
        private readonly IAppsModelServiceAPICall _appsModelService;
         

        [BindProperty]
        public AppsModels Models { get; set; }

        public CreateModel(IAppServices action, IAppsModelServiceAPICall appsModelService)
        {
            _action = action;
             _appsModelService = appsModelService;
            _appsModelService.SetVersion("v2");

            string token = "New Token";// HttpContext.Session.GetString( "access_token");
            _appsModelService.SetToken(token);
             
        }

        public IActionResult OnGet()
        {

            ViewData["Developers"] = CommonList.DeveloperList(_action);
            ViewData["GrantTypes"] = CommonList.GrantTypeList(_action);  //AccessMethodList;

            Models = new AppsModels();
            Models.TypeCode = "Implicit";
            Models.StartDate = DateTime.Now;
            Models.EndDate = DateTime.Now.AddDays(365);


            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
 
            var response = await _appsModelService.AddAsync<APIResponse>(Models);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Application Model added successfully";
                return RedirectToPage("./Index", new { appId = Models.AppID });
            }
            return Page();

        }
    }
}
