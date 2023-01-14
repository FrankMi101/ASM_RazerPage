using ASM.DataAccess;
using ASM.Models;
using ASM.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace ASM.Pages.AppUser.WorkingSchool
{
    
    public class IndexModel : PageModel
    {
        private readonly IAppServices _action; // IAppsRepository _db;
        public IEnumerable<School> Schools { get; set; }
        public IEnumerable<SelectListItem> AppsList { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }

        [BindProperty]
        public InputPara InputPara { get; set; } 
        public IndexModel(IAppServices action)
        {
            _action = action;
            AppsList = CommonList.AppsList(_action);
            RoleList = CommonList.UserRoleList(_action);
         
        }

        public void OnGet(string id)
        {
            InputPara inputPara = new InputPara();
            inputPara.UserID = HttpContext.User.Identity.Name;
            inputPara.StartDate = DateTime.Now;
            var endDate = DateTime.Now;
            inputPara.EndDate = endDate.AddDays(30);
            InputPara = inputPara;

            //if (panel == "All")
            //    Schools = _action.Schools.GetAll();// .GetAll(); // .ASM_Apps;
            //else
            //    Schools = _action.Schools.GetAll(s=>s.TypeCode == panel );

        }

        
        public   async Task<IActionResult> OnPostSearch( string searchby)
        {
            //if (searchby == "Panel")
            //    SearchList = PanelList;
            //else if (searchby == "Area")
            //    SearchList = AreaList;
            //else
            //    SearchList = DistrictList;

            return Page();

        }
    }
    public class InputPara
    {
        [Display(Name = "App Name")]
        public string AppsID { get; set; }

        [Display(Name = "User Role")]
        public string RoleID { get; set; }

        [Display(Name="Start Date")]
        //[DisplayFormat(DataFormatString = "{0:d}")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [BindProperty, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string UserID { get; set; }

    }
}
