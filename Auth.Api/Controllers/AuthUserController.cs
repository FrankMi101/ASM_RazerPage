using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        // GET: api/<AuthUserController>
        [Authorize]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var myheader = Request.Headers;

           // var Request = HttpContext.Request;
            string loginID = User.Identity.Name;
            string  role = User.FindFirstValue(ClaimTypes.Role).ToString();
            return new string[] { "value1", "value2", loginID, role };

            // need set up to  [ .AddJwtBearer(options => { options.SaveToken = true; }] in Program.cs
            // var token = HttpContext.GetTokenAsync("access_token").Result;

        }

       
    }
}
