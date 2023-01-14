namespace Auth.Api.Data
{
    public class AppSettingsCurrentDB
    {
        private static string _dbIndicatorKey = "x-DB-Indicater"; // from Http request header 

        public static string GetDbIndicator()
        {
            return _dbIndicatorKey;
        }
        public static string GetCurrentDB(string dbIndicator)
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);

            IConfiguration _configuration = builder.Build();
           return _configuration.GetConnectionString("CurrentDB");
        }
        public static void SetCurrentDB(string dbIndicator)
        {

        }
        private static string   GetConnectionStrbyCurrentDbIndicator(string? currenDB = null)
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

                IConfiguration _configuration = builder.Build();
                if (currenDB == null) currenDB = _configuration.GetConnectionString("CurrentDB");
                string currentDBConnectionStr = _configuration.GetConnectionString(currenDB);

                var appsettingV1 = _configuration.GetSection("AppSettings:AppServer").Value;
                var jwtsettingV1 = _configuration.GetSection("JwtSettings:JWTSecret").Value;


                return currentDBConnectionStr;
            }
        }
  
}
