using ASMWeb.DataAccess;
using ASMWeb.DataAccess.Repository;
using ASMWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASMWeb.Pages.AppUser.Staffs
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IAppActionsCatalog _action;
        public Employee Employee { get; set; }
        public IEnumerable<SelectListItem> SchoolList { get; set; } 

        public EditModel(IAppActionsCatalog action)
        {
            _action = action;
            SchoolList = CommonList.SchoolList(_action); 
        }

        public void OnGet(int id)
        {
            Employee = _action.Employees.GetById(id);
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
