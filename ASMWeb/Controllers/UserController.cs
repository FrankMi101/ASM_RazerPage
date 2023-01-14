using ASMWeb.DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASMWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IAppActionsCatalog _action;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserController(IAppActionsCatalog action, IWebHostEnvironment hostEnvironment)
        {
            _action = action;
            _hostEnvironment = hostEnvironment;
        }

 
        [HttpGet]
        public IActionResult Get()
        {
            var users = _action.ApplicationUser.GetAll();
            return Json(new { data = users });
        }

        [HttpPost]
        public IActionResult Post([FromBody] string id)
        {
            var objFromDb = _action.ApplicationUser.GetFirstOrDefault(u => u.Id == id);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(10);// .AddMonths(12);
            }
            _action.Save();
            return Json(new { success = true, message = "Lock or Unlock successful." });
        }

    }
}
