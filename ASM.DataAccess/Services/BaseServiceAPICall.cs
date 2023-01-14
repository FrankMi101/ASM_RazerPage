using ASM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Services
{
    public class BaseServiceAPICall : IBaseServiceAPICall
    {
        public APIResponse responseModel { get ; set; }
        public string Ver ;
        public string JWTToken ;

        public IHttpClientFactory httpClient { get; set; }
 
        public BaseServiceAPICall(IHttpClientFactory httpClient )
        {
            this.responseModel = new APIResponse();
            this.httpClient = httpClient;
         }
        public void SetToken(string token)
        {
            JWTToken = token;
        }
        public void SetVersion(string ver)
        {
            Ver  = ver;
        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
             //   apiRequest.Token = "";//  HttpContext.Session.GetString("JWTToken"); // "Bearer:token value";
                var client = httpClient.CreateClient("ASM.Api");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json"); 
                }
                switch (apiRequest.APIAction)
                {
                    case "POST":
                        message.Method = HttpMethod.Post; break;
                    case "PUT":
                        message.Method = HttpMethod.Put;break;
                    case "DELETE":
                        message.Method = HttpMethod.Delete;break;
                    default:
                        message.Method = HttpMethod.Get;break;  
                
                }

                HttpResponseMessage apiResponse = null;

                if (!string.IsNullOrEmpty(JWTToken) ) // if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                
                }

                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResult;
            }
            catch (Exception e) 
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };

                var res = JsonConvert.SerializeObject(dto);
                var apiResult = JsonConvert.DeserializeObject<T>(res);
                return apiResult;
            }
                    
        }
    }
}
