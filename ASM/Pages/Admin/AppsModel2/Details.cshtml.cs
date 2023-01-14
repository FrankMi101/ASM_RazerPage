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
using ASM.DataAccess.Services;
using Newtonsoft.Json;

namespace ASM.Pages.Admin.AppsModel2
{
    public class DetailsModel : PageModel
    {
        private readonly IAppServices _action;
        private readonly IAppsModelServiceAPICall _appsModelService;
        public DetailsModel(IAppServices action, IAppsModelServiceAPICall appsModelService)
        {
            _action = action;
            _appsModelService = appsModelService;
            _appsModelService.SetVersion("v2");
        }

        public AppsModels Models { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)   return NotFound();

            ViewData["Developers"] = CommonList.DeveloperList(_action);
            ViewData["GrantTypes"] = CommonList.GrantTypeList(_action);  //AccessMethodList;

            var respons = await _appsModelService.GetAsync<APIResponse>("IEP", id);
            if (respons != null && respons.IsSuccess)
            {
                Models = JsonConvert.DeserializeObject<AppsModels>(Convert.ToString(respons.Result));
            }
            else
            {
                return NotFound();
            }

            return Page();
        }
    }
}
