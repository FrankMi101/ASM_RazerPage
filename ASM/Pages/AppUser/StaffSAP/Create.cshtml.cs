#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASM.DataAccess;
using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM.Pages.AppUser.StaffSAP
{
    public class CreateModel : PageModel
    {
        private readonly IAppServices _action;

        [BindProperty]
        public SapProfile Profiles { get; set; }

        public IEnumerable<School> Schools { get; set; }
        public IEnumerable<SelectListItem> AppsList { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public IEnumerable<SelectListItem> GroupTypeList { get; set; }
        public IEnumerable<SelectListItem> GroupValueList { get; set; }

        public CreateModel(IAppServices action)
        {
            _action = action;
            AppsList = CommonList.AppsList(_action);
            RoleList = CommonList.UserRoleList(_action);
            GroupTypeList = CommonList.GrantGroupList(_action);
        }

        public string StaffUserId { get; set; }
        public IActionResult OnGet(string uId,string gType)
        {
            StaffUserId = uId;
            GroupValueList = CommonList.GrantValueList(_action, gType);
            Profiles = new SapProfile();
            Profiles.GroupType = gType;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var para = new { Operate = "Add", Id = 0,
                UserID = HttpContext.User.Identity.Name,
                GroupType = Profiles.GroupType,
                GroupValue = Profiles.GroupValue,
                StaffUserID = Profiles.StaffUserID,
                AppID = Profiles.AppID,
                AppRole = Profiles.AppRole,
                StartDate = Profiles.StartDate,
                EndDate = Profiles.EndDate,
                Comment = Profiles.Comments

            };
           var result = _action.SapProfiles.ValueOfT("Edit", para);
          

            TempData["success"] = "Application Model added successfully";
            return RedirectToPage("./Index" , new { Id = Profiles.StaffUserID });
 
        }
        public IActionResult OnPostGroupTypeChange(string gType)
        {
            gType = Profiles.GroupType;
            GroupValueList = CommonList.GrantValueList(_action, gType);
            var returnPara = new { uId = StaffUserId, gType = gType };
            return RedirectToPage("Create", returnPara); 
        }
    }
}
