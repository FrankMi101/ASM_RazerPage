using Microsoft.AspNetCore.Mvc;
using ASM.DataAccess.Repository;
using ASM.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASM.API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {

        private readonly IAppServices _action;
        public StaffsController(IAppServices action)
        {
            _action = action;
        }

        // GET: api/<StaffsController>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<NameValue>> Get()
        {
           var result = commonLists();
            return Ok(result);
        }

        [HttpGet("{schoolCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(string schoolCode)
        {
            var result = _action.Employees.GetAll(e => e.UnitID == schoolCode);
            return Ok(result);
        }

        [HttpGet("{searchBy}/{searchValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(string searchBy, string searchValue)
        {
            //***************** Dapper SP Call **********************************************
            var para = new { @Operate = "Staff", SearchBy = searchBy, SearchValue = searchValue, ScopeValue = "0000" };
            var result = _action.Employees.ListOfT("", para);
            return Ok(result);

        }


        [HttpGet("{searchBy}/{searchValue}/{scope}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(string searchBy, string searchValue, string scope)
        {
            //***************** Dapper SP Call **********************************************
            var para = new { @Operate = "Staff", SearchBy = searchBy, SearchValue = searchValue, ScopeValue = scope };
            var result = _action.Employees.ListOfT("", para);
            return Ok(result);

        }

        private List<NameValue> commonLists()
        {
            var cl = new List<NameValue>();
            cl.Add(new NameValue { Value = "TPA", Name = "Staff TPA Teacher" });
            cl.Add(new NameValue { Value = "NTP", Name = "Staff NTIP Teacher" });
            cl.Add(new NameValue { Value = "LTO", Name = "Staff LTO Teacher" });
            cl.Add(new NameValue { Value = "NE", Name = "Staff NE Teacher" });
            return cl;
        }
    }
}