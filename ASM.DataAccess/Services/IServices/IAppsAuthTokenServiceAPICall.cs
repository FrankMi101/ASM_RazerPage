using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Services
{
    public interface IAppsAuthTokenServiceAPICall :IBaseServiceAPICall
    {
        Task<T> GetToken<T>(ClaimsPrincipal claimsP);
    }
}
