using ASM.DataAccess;
using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASM.Pages.AppUser.Staffs
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IAppServices _action;
        public Employee Employee { get; set; }
        public IEnumerable<SelectListItem> SchoolList { get; set; } 

        public EditModel(IAppServices action)
        {
            _action = action;
            SchoolList =  CommonList.SchoolList(_action); 
        }

        public void OnGet(string id)
        {
           //  Employee = _action.Employees.GetById(id);
            var para = new { Operate = "Get", UserID = id };
            Employee = _action.Employees.ObjectOfT("Read", para);

        }
        public async Task<IActionResult> OnPost()
        {
           // Customer Validation
           
            if (ModelState.IsValid) // Required server side validation
            {
                _action.Employees.Update(Employee); 
                _action.Save();  
                TempData["success"] = "Application Update successfully";
                return RedirectToPage("Index");
           }
            return Page();
        }
    }
}
