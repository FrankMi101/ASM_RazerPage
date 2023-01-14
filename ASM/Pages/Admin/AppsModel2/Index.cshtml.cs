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
using Microsoft.AspNetCore.Authentication;

namespace ASM.Pages.Admin.AppsModel2
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
            _appsModelService.SetVersion("v2"); // v1 does not work, because V1 API returns data are not an APIResponse object
            string token = HttpContext.Session.GetString("access_token");
            _appsModelService.SetToken(token);
        }

        public IList<AppsModels> Models { get;set; }

        public async Task OnGetAsync(string appId)
        {
          

            ViewData["Developers"] = CommonList.DeveloperList(_action);
            ViewData["GrantTypes"] = CommonList.GrantTypeList(_action);  //AccessMethodList;

            //if (appId != null)
            //{  ViewData["WorkingAppId"] = appId; }
            //else
            //{ appId = ViewData["WorkingAppId"].ToString();}
                 
            if (appId !=null)
            {
                var respons = await _appsModelService.GetAllAsync<APIResponse>(appId);
                if (respons != null && respons.IsSuccess)
                {
                    Models = JsonConvert.DeserializeObject<List<AppsModels>>(Convert.ToString(respons.Result));
                }

            }
             else
            {
                Models = _action.AppModels.GetAll().ToList();// await _action.ASM_AppModels.Where(m => m.AppID == appId).ToListAsync();

            }

        }
    }
}
