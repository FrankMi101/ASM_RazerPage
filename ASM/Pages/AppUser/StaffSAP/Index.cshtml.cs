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

namespace ASM.Pages.AppUser.StaffSAP
{
    //[Authorize(Roles = $"{"Admin"},{"Support"}")]
    public class IndexModel : PageModel
    {
        private readonly IAppServices _action;
        public IList<SapProfile> SapProfileList { get; set; }
        public IEnumerable<SelectListItem> UserRoleList { get; set; }

        public IndexModel(IAppServices action)
        {
            _action = action;
        }

        public async Task OnGetAsync(string Id)
        {
            UserRoleList = CommonList.UserRoleList(_action);
            var para = new { Operate = "SAP", UserID =Id };
            SapProfileList = _action.SapProfiles.ListOfT("Read", para).ToList();
         

        }
    }
}
