using ASM.DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : Controller
    {

        private readonly IAppServices _action;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SchoolController(IAppServices action, IWebHostEnvironment hostEnvironment)
        {
            _action = action;
            _hostEnvironment = hostEnvironment;
        }

 
        [HttpGet]
        public IActionResult Get()
        {


            // var schoolList = _action.Schools.GetAll(includeProperties: "Principal,Area,District,SchoolType");
            var para = new { Operate = "SchoolList",Para1 = "All" };
            var schoolList = _action.Schools.ListOfT("", para); // _action.Schools.GetAll(); 
            
            return Json(new { data = schoolList });
        }


        [HttpGet("{panel}")]
        public IActionResult Get(string panel)
        {   // *********  Store procedure **************************
            var para = new { Operate = "SchoolList", Para1 = "Panel",Para2 = panel };
            var schoolList = _action.Schools.ListOfT("", para); // _action.Schools.GetAll();
            return Json(new { data = schoolList });
            // ********************************************************
            /*
            var schoolList = _action.Schools.GetAll();

            if (panel != "All")
                schoolList = schoolList.Where(s => s.TypeCode == panel);
            // _action.Schools.GetAll(s => s.TypeCode == panel);
            return Json(new { data = schoolList });

            // var schoolList = _action.Schools.GetAll(includeProperties: "Principal,Area,District,SchoolType");
           // var schoolList = _action.Schools.GetAll(s => s.TypeCode == panel);
            */
        }

        [HttpGet("{searchBy}/{searchValue}")]
        public IActionResult Get(string searchBy, string searchValue)
        {   
            // ************* strore procedure Call via Dapper ********************************
            var para = new { Operate = "SchoolList", Para1 = searchBy, Para2 = searchValue };
            var schoolList = _action.Schools.ListOfT("", para); // _action.Schools.GetAll();
            return Json(new { data = schoolList });
            // ****************************************************************************
         
            /* ************ Entity Framework Call ***********************************************
            var schoolList = _action.Schools.GetAll();

            if (searchBy == "Panel" )
            {
                if (searchValue != "All")
                schoolList = schoolList.Where(s => s.TypeCode == searchValue);
            }
            if (searchBy == "Area")
            {
                if (searchValue != "All")
                    schoolList = schoolList.Where(s => s.AreaCode == searchValue);
            }
            if (searchBy == "District")
            {
                if (searchValue != "All")
                    schoolList = schoolList.Where(s => s.DistrictCode == searchValue);
            }
          
            return Json(new { data = schoolList });
            */
         }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _action.Schools.GetFirstOrDefault(u => u.Id == id);

            _action.Schools.Remove(objFromDb);
            _action.Save();
            return Json(new { success = true, message = "Delete successful." });
        }

        // GET: SchoolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SchoolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
 

        // POST: SchoolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
