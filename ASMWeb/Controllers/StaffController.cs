using ASMWeb.DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASMWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : Controller
    {

        private readonly IAppActionsCatalog _action;
        private readonly IWebHostEnvironment _hostEnvironment;

        public StaffController(IAppActionsCatalog action, IWebHostEnvironment hostEnvironment)
        {
            _action = action;
            _hostEnvironment = hostEnvironment;
        }

 
        [HttpGet]
        public IActionResult Get()
        {
             var employees = _action.Employees.GetAll();
            return Json(new { data = employees });
        }

        [HttpGet("{schoolcode}")]
        public IActionResult Get(string schoolCode)
        {
            // var schoolList = _action.Schools.GetAll(includeProperties: "Principal,Area,District,SchoolType");
            var employees = _action.Employees.GetAll(e => e.UnitID == schoolCode);
            return Json(new { data = employees });
        }

        [HttpGet("{searchValue}/{searchby}")]
        public IActionResult Get(string searchValue, string searchBy)
        {
            //***************** Dapper SP Call **********************************************
            var para = new { @Operate = "Staff", SearchBy = searchBy, SearchValue = searchValue, ScopeValue = "0000" };
            var employeeList = _action.Employees.ListOfT("", para);
            return Json(new { data = employeeList });
            // ********************************************************************************
            /**************************** EF  *********************************
               var employeeList = _action.Employees.GetAll();

               if (searchBy == "LastName")
                   employeeList =   employeeList.Where(s => s.LastName.Contains(searchValue)); //  _action.Employees.GetAll(e => e.LastName.Contains(searchValue));
               else if (searchBy == "FirstName")
                   employeeList = employeeList.Where(s => s.FirstName.Contains(searchValue)); // _action.Employees.GetAll(e => e.FirstName.Contains(searchValue));
               else if (searchBy == "UserID")
                   employeeList = employeeList.Where(s => s.UserID == searchValue); // _action.Employees.GetAll(e => e.UserID == searchValue);
               else if (searchBy == "School")
                   employeeList = employeeList.Where(s => s.UnitID == searchValue); // _action.Employees.GetAll(e => e.UnitID == searchValue);
               else if (searchBy == "EmployeeID")
                   employeeList = employeeList.Where(s => s.EmployeeID == searchValue); // _action.Employees.GetAll(e => e.EmployeeID == searchValue);

               return Json(new { data = employeeList });
           */
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _action.Employees.GetFirstOrDefault(u => u.Id == id);

            _action.Employees.Remove(objFromDb);
            _action.Save();
            return Json(new { success = true, message = "Delete successful." });
        }

    }
}
