
using ASM.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.DataAccess.Repository
{
    public class SP_Call : ISP_Call
    { 
        private readonly ApplicationDbContext _db;
        private static string ConString = "";

        public SP_Call()
        {         
            ConString = GetConnectionStrFromAppSettingsbyDbIndicator();
        }
        public SP_Call(ApplicationDbContext db)
        {
            _db = db;
            ConString = db.Database.GetDbConnection().ConnectionString;
        }
        public SP_Call(string dbIndicator)
        {
         
            ConString = GetConnectionStrFromAppSettingsbyDbIndicator(dbIndicator); 
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

        public T ExecuteReturn<T>(string spName, object? para = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var data = sqlCon.QuerySingle<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return data; //  (T)Convert.ChangeType(data, typeof(T));
                 
            }
        }
        public T ReturnValue<T>(string spName, object? para = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var data = sqlCon.QuerySingle<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            }
        }
        public string ReturnValue(string spName, object? para = null)
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
                var data = sqlCon.QuerySingle<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return data; //  (T)Convert.ChangeType(data, typeof(T));
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
        public List<T> ReturnList<T>(string spName, object? para = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var list = sqlCon.Query<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return list.ToList();
            }
        }

        private string GetConnectionStrFromAppSettingsbyDbIndicator(string ? currenDB = null)
        {
            //string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "appSetting.json");
            //IConfiguration myConfig = new ConfigurationBuilder()
            //                           .SetBasePath(Path.GetDirectoryName(filePath))
            //                           .AddJsonFile("appSettings.json")
            //                           .Build();
            //string myValue = myConfig.GetSection("nameOfMyValue").ToString();

            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);
             
            IConfiguration  _configuration = builder.Build();
            if (currenDB == null) currenDB = _configuration.GetConnectionString("CurrentDB");
            string currentDBConnectionStr = _configuration.GetConnectionString(currenDB);

           // var appsettingV1 = _configuration.GetSection("AppSettings:AppServer").Value;
           // var jwtsettingV1 = _configuration.GetSection("JwtSettings:JWTSecret").Value;


            return currentDBConnectionStr;
        }

        public List<T> ReturnList<T>(string spName, object? para = null, string? db = null)
        {    if (db != null)  ConString = GetConnectionStrFromAppSettingsbyDbIndicator(db);
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var list = sqlCon.Query<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return list.ToList();
            }
        }

        public T ExecuteReturn<T>(string spName, object? para = null, string? db = null)
        {
            if (db != null)  ConString = GetConnectionStrFromAppSettingsbyDbIndicator(db);
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var data = sqlCon.QuerySingle<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return data; //  (T)Convert.ChangeType(data, typeof(T));
            }
        }

        public T ReturnValue<T>(string spName, object? para = null, string? db = null)
        {
            return ExecuteReturn<T>(spName, para, db);
            
        }

        public string ReturnValue(string spName, object? para = null, string? db = null)
        {
            if (db != null) ConString = GetConnectionStrFromAppSettingsbyDbIndicator(db);
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var data = sqlCon.QuerySingle<string>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            }
        }

        public async Task<List<T>> ReturnListAsync<T>(string spName, object? para = null, string? db = null)
        {
            if (db != null) ConString = GetConnectionStrFromAppSettingsbyDbIndicator(db);
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var list = sqlCon.QueryAsync<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return (List<T>) await list;
            }
        }

        public async Task<T> ReturnValueAsync<T>(string spName, object? para = null, string? db = null)
        {
            if (db != null) ConString = GetConnectionStrFromAppSettingsbyDbIndicator(db);
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var data = sqlCon.QuerySingleAsync<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return (T)await data; //  (T)Convert.ChangeType(data, typeof(T));
            }
        }
        public async Task<T> ReturnObjectAsync<T>(string spName, object? para = null, string? db = null)
        {
            if (db != null) ConString = GetConnectionStrFromAppSettingsbyDbIndicator(db);
            using (SqlConnection sqlCon = new SqlConnection(ConString))
            {
                sqlCon.Open();
                var data = sqlCon.QuerySingleAsync<T>(spName, para, commandType: System.Data.CommandType.StoredProcedure);
                return (T)await data; //  (T)Convert.ChangeType(data, typeof(T));
            }
        }
    }
}
