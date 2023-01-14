using ASM.DataAccess.Repository;
using ASM.DataAccess.Services;
using ASM.Models;
using Auth.Api.Data;
using Authen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IOptions<AppConfigAppSettings> _appSettings;
        private readonly IOptions<AppConfigJwtSettings> _jwtSettings;
        private readonly IAppServices _action;
        private readonly IAuthService _auth;
        // private readonly IRequestContext _requestContext = null;

        public AuthenticationController(IOptions<AppConfigAppSettings> appSettings,
                        IOptions<AppConfigJwtSettings> jwtSettings, IAppServices action, IAuthService auth)
        {
            //  _logger = logger;
            _appSettings = appSettings;
            _jwtSettings = jwtSettings;
            _action = action;
            _auth = auth;
            // _requestContext = context;

            //  _dbIndicator = Request.Headers["x-DB-Value"];
        }



        // GET: api/<TokenController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var dbIndicator = AppSettingsCurrentDB.GetDbIndicator();
            var _dbIndicator = Request.Headers[dbIndicator];
            return new string[] { "Auth Api value1", "Auth Api value2", _dbIndicator };
        }

        // GET api/<TokenController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            // Check Authentication

            // Step 1 Get jwt token from the request header
            var Request = HttpContext.User.Identity.Name; //  .Request.Headers ; // new HttpRequestMessage();

            // string authHeader = httpContext.Request.Headers["Authorization"];



            //   var headers = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last(); ;

            // if (Request.Authorization == null) return BadRequest();

            //var jwtToken = Request.Headers.Authorization.Parameter;

            // if (jwtToken == null) return BadRequest();

            // var jwtManager = new JWTManager(_jwtSettings, _action);

            // if (!jwtManager.ValidateToken(Request)) return Unauthorized();

            return   Ok(Request);
        }

        // POST api/<TokenController>
        [HttpPost("{authenticate}")]
        public async Task<ActionResult<string>> Authenticate([FromBody] AuthUser authuser)
        {

            // Stpe 1 authenticate the User 
            // IAuthentication auth  = new Authentication(_appSettings);
            //  if (!auth.IsAuthenticated(authuser))  return Unauthorized();


            // if (!_auth.Authencate.IsAuthenticated(authuser)) return Unauthorized();
            var IsAuthed = await _auth.Authencate.IsAuthenticatedAsync(authuser);
            if (!IsAuthed) return Unauthorized();


            // Step 2 Get JWT token 
            // IJWTManager jwtManager = new JWTManager(_jwtSettings,_action);
            // var token = jwtManager.CreateToken(authuser);

            SetDbIndicator();

           // var token = _auth.JwtManagers.CreateToken(authuser);
            var token = await _auth.JwtManagers.CreateTokenAsync(authuser);
            return Ok(token);
        }
        private void SetDbIndicator()
        {
            var dbIndicator = AppSettingsCurrentDB.GetDbIndicator();
            var _dbIndicator = Request.Headers[dbIndicator];
            AppSettingsCurrentDB.SetCurrentDB(_dbIndicator);
        }

        private string ReadHTTPRequestHeader(HttpRequest request)
        {
            var headers = String.Empty;
            foreach (var key in Request.Headers.Keys)
                headers += key + ":=" + Request.Headers[key] + Environment.NewLine;

            return headers;

        }
    }
}
