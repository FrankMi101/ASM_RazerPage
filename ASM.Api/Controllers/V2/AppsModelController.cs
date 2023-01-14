using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
//using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASM.Api.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
   // [Route("api/[controller]")]
    [ApiController]

    public class AppsModelController : ControllerBase
    {
        private readonly IAppServices _action;
        protected APIResponse _response;
        public AppsModelController(IAppServices action)
        {
            _action = action;
            _response = new();
        }


        // GET: api/<AppsModelController>
        [HttpGet("{appId}")]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<ActionResult<APIResponse>> GetAll(string appId)
        {
            try
            {
                _response.Result = await _action.AppModels.ListOfTAsync("Read", new { AppId = appId });
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return _response;
            }
        }

        // GET api/<AppsModelController>/5
        [HttpGet("{appId}/{id}")]
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<ActionResult<APIResponse>> Get(string appId, int id)
        {
            try
            {
                _response.Result = await _action.AppModels.ObjectOfTAsync("Read", new { AppId = appId, Id = id });
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return _response;
            }
        }

        // POST api/<AppsModelController>
        [HttpPost]
        public async Task<ActionResult<APIResponse>> PostAsync([FromBody] AppsModels models)
        { // Insert Db new record
            try
            {
                if (models != null)
                {
                    await _action.AppModels.AddAsync(models);
                    _action.Save();
                }
                _response.Result = models;
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return _response;
            }
        }

        // PUT api/<AppsModelController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<APIResponse>> Put(int id, [FromBody] AppsModels models)
        { // Update Db record
            try
            {
                if (models != null)
                {
                    await _action.AppModels.UpdateAsync(models);
                    _action.Save();

                    _response.Result = models;
                    _response.StatusCode = HttpStatusCode.Accepted;
                    return Ok(_response);


                }
                else
                {
                    return _response;
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return _response;
            }
        }

        // DELETE api/<AppsModelController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                else
                {
                    var model = await _action.AppModels.ObjectOfTAsync("Read", new { AppID = "", Id = id });
                    if (model == null)
                    {
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.Result = "Object not found";
                        return NotFound(_response);
                    }

                    else
                    {
                        var para = new AppsModels { Id = id };
                        await _action.AppModels.RemoveAsync(para);
                        _action.Save();
                        _response.StatusCode = HttpStatusCode.NoContent;
                        _response.IsSuccess = true;
                    }
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return _response;
            }
        }
    }
}

