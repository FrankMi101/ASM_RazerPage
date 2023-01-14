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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using ASM.DataAccess.Services;
using Newtonsoft.Json;

namespace ASM.Pages.Admin.AppsModel
{
    //[Authorize(Roles = $"{"Admin"},{"Support"}")]
    public class IndexModel : PageModel
    {
         private readonly IAppServices _action;
        private readonly IAppsModelServiceAPICall _appsModelService;
        public IndexModel(IAppServices action, IAppsModelServiceAPICall appsModelService)
        {
            _action = action;
            _appsModelService = appsModelService;
            _appsModelService.SetVersion("v1");
        }

        public IList<AppsModels> Models { get;set; }

        public async Task OnGetAsync(string appId)
        {
            ViewData["Developers"] =  CommonList.DeveloperList(_action);
            ViewData["GrantTypes"] = CommonList.GrantTypeList(_action);  //AccessMethodList;

            //if (appId != null)
            //{  ViewData["WorkingAppId"] = appId; }
            //else
            //{ appId = ViewData["WorkingAppId"].ToString();}

            if (appId != null)
                Models = _action.AppModels.GetAll(m => m.AppID == appId).ToList();// await _action.ASM_AppModels.Where(m => m.AppID == appId).ToListAsync();
            else
                Models = _action.AppModels.GetAll().ToList();// await _action.ASM_AppModels.Where(m => m.AppID == appId).ToListAsync();


        }
    }
}
