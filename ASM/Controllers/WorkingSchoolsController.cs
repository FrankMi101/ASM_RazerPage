using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingSchoolsController : Controller
    {

        private readonly IAppServices _action;
        private readonly IWebHostEnvironment _hostEnvironment;

        public WorkingSchoolsController(IAppServices action, IWebHostEnvironment hostEnvironment)
        {
            _action = action;
            _hostEnvironment = hostEnvironment;
        }


        [HttpGet]
        public IActionResult Get()
        {
            // var schoolList = _action.Schools.GetAll(includeProperties: "Principal,Area,District,SchoolType");
            var para = new { Operate = "SchoolList", SearchBy = "District", SearchValue = "Selected" };
            var schoolList = _action.WorkingSchools.ListOfT("Read", para); // _action.Schools.GetAll();

            return Json(new { data = schoolList });
        }



        [HttpGet("{searchBy}/{searchValue}")]
        public IActionResult Get(string searchBy, string searchValue)
        {
            // ************* strore procedure Call via Dapper ********************************
            var para = new { Operate = "SchoolList", SearchBy = searchBy, SearchValue = searchValue };
            var schoolList = _action.WorkingSchools.ListOfT("Read", para);
            return Json(new { data = schoolList });
            // ****************************************************************************

        }



        [HttpGet("{userId}/{searchBy}/{searchValue}/{employeeId}")]
        public IActionResult Get(string userId, string searchBy, string searchValue, string employeeId)
        {
            // ************* strore procedure Call via Dapper ********************************
            var para = new { Operate = "SchoolList", UserID = userId, SearchBy = searchBy, SearchValue = searchValue, EmployeeID = employeeId };
            var schoolList = _action.WorkingSchools.ListOfT("Read", para); ;
            return Json(new { data = schoolList });
            // ****************************************************************************

        }


        [HttpPost]
        public IActionResult Post([FromBody] WorkingSchoolsPara obj)
        {
            var toFrom = " to ";
            if (obj.Action == "Delete") toFrom = " from ";

            try
            {
                var para = new { obj.Operate, Id = 0, obj.UserID, obj.GroupType, obj.GroupValue, obj.StaffUserID, obj.AppID, obj.AppRole, obj.StartDate, obj.EndDate };
                var result = _action.WorkingSchools.ValueOfT("Edit", para); // _action.Schools.GetAll();
                bool resultbool = false;
                if (result.Contains("Successfully")) resultbool = true;
                return Json(new { success = resultbool, message = obj.Action + " School " + toFrom + obj.StaffUserID + " " + result });

            }
            catch
            {
                return Json(new { success = false, message = obj.Action + " School " + toFrom + obj.StaffUserID + " failed" });
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _action.WorkingSchools.GetFirstOrDefault(u => u.Id == id);

            _action.WorkingSchools.Remove(objFromDb);
            _action.Save();
            return Json(new { success = true, message = "Delete successful." });
        }
    }
    public class WorkingSchoolsPara
    {
        public string Operate { get; set; }
        public string Action { get; set; }
        public string UserID { get; set; }
        public string GroupType { get; set; }
        public string GroupValue { get; set; }
        public string StaffUserID { get; set; }
        public string AppID { get; set; }
        public string AppRole { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
