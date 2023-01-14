using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASM.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class NameValueListController : ControllerBase
    {

        private readonly IAppServices _action;
        public NameValueListController(IAppServices action)
        {
            _action = action;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<PositionType>> Get()
        {
            var para = new { Operate = "UserRole", Para1 = "Admin" };
            var result = _action.NameValueItems.ListOfT("", para);
            return Ok(result);
        }


        // GET: api/<PositionTypeController>
        [HttpGet("{type}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<NameValue>> Get(string type)
        {
            var para = new { Operate = type };
            var result = _action.NameValueItems.ListOfT("", para);
            return Ok(result);
        }

       
    }
}
