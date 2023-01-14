using ASM.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ASM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IAppServices _action;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EmployeeController(IAppServices action, IWebHostEnvironment hostEnvironment)
        {
            _action = action;
            _hostEnvironment = hostEnvironment;
        }

 
        [HttpGet]
        public IActionResult Get()
        {
            // var schoolList = _action.Schools.GetAll(includeProperties: "Principal,Area,District,SchoolType");
            var employeeList = _action.Employees.GetAll(e => e.UnitID == "0529");
            return Json(new { data = employeeList });
        }


        //[HttpGet("{school}")]
        //public IActionResult Get(string schoolCode)
        //{
        //    // var schoolList = _action.Schools.GetAll(includeProperties: "Principal,Area,District,SchoolType");
        //    var employeeList = _action.Employees.GetAll(e => e.UnitID == schoolCode);
        //    return Json(new { data = employeeList });
        //}
        //[HttpGet("{searchValue}/{searchby}")]
        //public IActionResult Get(string searchValue, string searchBy)
        //{
        //     var employeeList = _action.Employees.GetAll(e => e.EmployeeID == searchValue);

        //    if (searchBy == "LastName")
        //        employeeList = _action.Employees.GetAll(e => e.LastName == searchValue);
        //    else if (searchBy == "FirstName")
        //        employeeList = _action.Employees.GetAll(e => e.FirstName == searchValue);
        //    else if (searchBy == "UserID")
        //        employeeList = _action.Employees.GetAll(e => e.UserID == searchValue);
        //    else
        //        employeeList = _action.Employees.GetAll(e => e.EmployeeID == searchValue);

        //    return Json(new { data = employeeList });
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var objFromDb = _action.Schools.GetFirstOrDefault(u => u.Id == id);

        //    _action.Schools.Remove(objFromDb);
        //    _action.Save();
        //    return Json(new { success = true, message = "Delete successful." });
        //}
 
        //// POST: SchoolController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    }
}
