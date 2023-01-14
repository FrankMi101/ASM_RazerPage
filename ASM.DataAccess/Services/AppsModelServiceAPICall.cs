using ASM.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Services
{
    public class AppsModelServiceAPICall : BaseServiceAPICall, IAppsModelServiceAPICall
    {
        private readonly IHttpClientFactory _clientFactory;
        private string apiUrl;
        
        public AppsModelServiceAPICall(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            this.apiUrl = configuration.GetConnectionString("APIUrl");
         }
        public Task<T> AddAsync<T>(AppsModels model)
        {
            var apiRequest = new APIRequest()
            {
                APIAction = "POST",
                Data = model,
                Url = apiUrl + "/api/" + Ver + "/AppsModel",
             };
            return SendAsync<T>(apiRequest);
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            var apiRequest = new APIRequest()
            {
                APIAction = "DELETE",
                Url = apiUrl + "/api/" + Ver + "/AppsModel/" + id.ToString(),
             };
            return SendAsync<T>(apiRequest);
        }
      public Task<T> UpdateAsync<T>(AppsModels model)
        {
            var apiRequest = new APIRequest()
            {
                APIAction = "PUT",
                Data = model,
                Url = apiUrl + "/api/" + Ver + "/AppsModel/" + model.Id.ToString(),
             };
            return SendAsync<T>(apiRequest);
        }
        public Task<T> GetAllAsync<T>(string appId)
        {

            var apiRequest = new APIRequest()
            {
                APIAction = "GET",
                Url = apiUrl + "/api/" + Ver +"/AppsModel/" + appId.ToString(),
             };
            return SendAsync<T>(apiRequest);
        }

        public Task<T> GetAsync<T>(string appId, int id)
        {
            var apiRequest = new APIRequest()
            {
                APIAction = "GET",
                Url = apiUrl + "/api/" + Ver +"/AppsModel/" + appId + "/" + id.ToString(),
             };
            return SendAsync<T>(apiRequest);
        }
    }
}
