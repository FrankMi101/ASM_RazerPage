using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Services
{
    public interface IBaseServiceAPICall
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);

        void SetToken(string token);
        void SetVersion(string ver);

    }
}
