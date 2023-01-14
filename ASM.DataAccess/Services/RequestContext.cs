using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ASM.DataAccess.Services
{
    public sealed class RequestContext : IRequestContext
    {
        private readonly IHttpContextAccessor _accessor;
        public RequestContext(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public string CorrelationId
        {
            get { return _accessor.HttpContext.Request.Headers["x-DB-Value"]; }
        }
    }
}
