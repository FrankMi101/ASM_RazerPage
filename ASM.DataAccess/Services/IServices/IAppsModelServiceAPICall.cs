using ASM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Services
{
    public interface IAppsModelServiceAPICall :IBaseServiceAPICall
    {
        Task<T> GetAllAsync<T>(string appId);
        Task<T> GetAsync<T>(string appId,int id);
        Task<T> AddAsync<T>(AppsModels model);
        Task<T> UpdateAsync<T>(AppsModels model);
        Task<T> DeleteAsync<T>(int id);
    }
}
