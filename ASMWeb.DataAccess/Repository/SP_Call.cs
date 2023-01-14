using ASMWeb.DataAccess.Repository;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMWeb.DataAccess.Repository
{
    public class SP_Call : ISP_Call
    { 
        private readonly ApplicationDbContext _db;
        private static string ConString = "";

        public SP_Call(ApplicationDbContext db)
        {
            _db = db;
            ConString = db.Database.GetDbConnection().ConnectionString;

        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void ExecuteNoReturn(string spName, DynamicParameters para = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                sqlCon.Execute(spName, para, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public T ExecuteReturn<T>(string spName, object para = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var data = sqlCon.Execute(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return (T)Convert.ChangeType(data, typeof(T));
            }
        }

        public string ReturnValue(string spName, object para = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var data = sqlCon.QuerySingle<string>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return   data;
            }
        }
        public T ExecuteReturn<T>(string spName, DynamicParameters para = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var data = sqlCon.Execute(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return (T)Convert.ChangeType(data, typeof(T));
            }
        }

        public List<T> ReturnList<T>(string spName, DynamicParameters para = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var list = sqlCon.Query<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return list.ToList();
            }
        }
        public List<T> ReturnList<T>(string spName, object para = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var list = sqlCon.Query<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return list.ToList();
            }
        }
    }
}
