using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASM.Api.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class NameValueListController : ControllerBase
    {

        private readonly IAppServices _action;
        public NameValueListController(IAppServices action)
        {
            _action = action;
        }

        // GET: api/<PositionTypeController>
        [HttpGet("{type}/{para1}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<NameValue>> Get(string type, string para1)
        {
            var para = new { Operate = type, Para1 = para1 };
            var result = _action.NameValueItems.ListOfT("", para);
            return Ok(result);
        }

        // GET: api/<PositionTypeController>
        [HttpGet("{type}/{para1}/{para2}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<NameValue>> Get(string type, string para1, string para2)
        {
            var para = new { Operate = type, Para1 = para1, Para2 = para2 };
            var result = _action.NameValueItems.ListOfT("", para);
            return Ok(result);
        }
        // GET: api/<PositionTypeController>
        [HttpGet("{type}/{para1}/{para2}/{para3}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<NameValue>> Get(string type, string para1, string para2, string para3)
        {
            var para = new { Operate = type, Para1 = para1, Para2 = para2, Para3 = para3 };
            var result = _action.NameValueItems.ListOfT("", para);
            return Ok(result);
        }
    }
}
