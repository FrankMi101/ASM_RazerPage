using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public interface ISP_Call:IDisposable
    {
        List<T> ReturnList<T>(string spName, object para = null);
        T ExecuteReturn<T>(string spName, object para = null);
        List<T> ReturnList<T>(string spName, DynamicParameters para = null);
        void ExecuteNoReturn(string spName, DynamicParameters para = null);
        T ExecuteReturn<T>(string spName, DynamicParameters para = null);
    }
}
