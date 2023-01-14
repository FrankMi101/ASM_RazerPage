using ASM.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Services
{
    internal class AppsAuthTokenServiceAPICall : BaseServiceAPICall, IAppsAuthTokenServiceAPICall
    {
        private readonly IHttpClientFactory _clientFactory;
        private string apiUrl ;

        public AppsAuthTokenServiceAPICall(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            this.apiUrl = configuration.GetConnectionString("AuthAPIUrl");
        }
        public Task<T> GetToken<T>(ClaimsPrincipal claimsP)
        {

            var apiRequest = new APIRequest()
            {
                APIAction = "POST",
                Url = apiUrl + "/api/" + Ver + "/Authentication/Authenticate",
            };
            return SendAsync<T>(apiRequest);
        }
    }
}
