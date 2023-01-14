using ASM.DataAccess.Repository;
using ASM.Models;
using Auth.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IAppServices _action;
        private readonly ILogging _logger;
        public StaffsController(IAppServices action, ILogging logger)
        {
            _action = action;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/<StaffsController>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<NameValue>>> Get()
        {
             _logger.Log("Information here", "Info");
            var result = await commonLists();
            return Ok(result);
        }
        [Authorize]
        [HttpGet("{schoolCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeebySchool(string schoolCode)
        {
            //var result = _action.Employees.GetAll.GetAll(e => e.UnitID == schoolCode);
            var result = await _action.Employees.GetAllAsync(e => e.UnitID == schoolCode);
            if (result == null)
            {
                _logger.Log($"Erro Message here {schoolCode} Not exist", "Error");
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("{searchBy}/{searchValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeebySearch(string searchBy, string searchValue)
        {
            //***************** Dapper SP Call **********************************************
            var para = new { @Operate = "Staff", SearchBy = searchBy, SearchValue = searchValue, ScopeValue = "0000" };
            var result = await  _action.Employees.ListOfTAsync("", para); //.Employees.ListOfT("", para);
            return Ok(result);

        }


        [HttpGet("{searchBy}/{searchValue}/{scope}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeebySearchInScope(string searchBy, string searchValue, string scope)
        {
            //***************** Dapper SP Call **********************************************
            var para = new { @Operate = "Staff", SearchBy = searchBy, SearchValue = searchValue, ScopeValue = scope };
            var result = await  _action.Employees.ListOfTAsync("", para);
            return Ok(result);

        }
        private async Task<List<NameValue>> commonLists()
        {
            var cl = new List<NameValue>();
            cl.Add(new NameValue { Value = "TPA", Name = "Staff TPA Teacher" });
            cl.Add(new NameValue { Value = "NTP", Name = "Staff NTIP Teacher" });
            cl.Add(new NameValue { Value = "LTO", Name = "Staff LTO Teacher" });
            cl.Add(new NameValue { Value = "NE", Name = "Staff NE Teacher" });
            return   cl;
        }
    }
}
