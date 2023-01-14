using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.AspNetCore.Mvc;
 
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASM.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
  //  [Route("api/[controller]")]
    [ApiController]
    public class AppsModelController : ControllerBase
    {
        private readonly IAppServices _action;

        public AppsModelController(IAppServices action)
        {
            _action = action;
        }


        // GET: api/<AppsModelController>
        [HttpGet("{appId}")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AppsModels>>> Get(string appId)
        {
            return Ok(await _action.AppModels.ListOfTAsync("Read", new { AppId = appId }));
        }

        // GET api/<AppsModelController>/5
        [HttpGet("{appId}/{id}")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppsModels>> Get(string appId, int id)
        {
            return Ok(await _action.AppModels.ObjectOfTAsync("Read", new { AppId = appId, Id = id }));
        }

        // POST api/<AppsModelController>
        [HttpPost]
        public async Task PostAsync([FromBody] AppsModels models)
        {
            if (models != null)
            {
                int id = models.Id;
                await _action.AppModels.UpdateAsync(models);
                _action.Save();
            }
        }

        // PUT api/<AppsModelController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] AppsModels models)
        {
            if (models != null)
            {
                await _action.AppModels.AddAsync(models);
                _action.Save();
            }

        }

        // DELETE api/<AppsModelController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (id != null)
            {
                var para = new AppsModels { Id = id };
                await _action.AppModels.RemoveAsync(para);
                _action.Save();
            }
        }
    }
}
