using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public interface ISP_Call:IDisposable
    {
        List<T> ReturnList<T>(string spName, object? para = null, string? db = null );
        T ExecuteReturn<T>(string spName, object? para = null, string? db = null);
        T ReturnValue<T>(string spName, object? para = null, string? db = null);
        string ReturnValue(string spName, object? para = null, string? db = null);

        List<T> ReturnList<T>(string spName, DynamicParameters para = null);
        void ExecuteNoReturn(string spName, DynamicParameters para = null);
        T ExecuteReturn<T>(string spName, DynamicParameters para = null);

        Task<List<T>> ReturnListAsync<T>(string spName, object? para = null, string? db = null);
         Task<T> ReturnValueAsync<T>(string spName, object? para = null, string? db = null);
        Task<T> ReturnObjectAsync<T>(string spName, object? para = null, string? db = null);
    }
}
