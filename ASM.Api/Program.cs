using ASM.Api.Logging;
using ASM.DataAccess;
using ASM.DataAccess.Repository;
using ASM.Models;
using Authen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DB connection
var currentDB = builder.Configuration.GetConnectionString("CurrentDb");
var connectStr = builder.Configuration.GetConnectionString(currentDB);
// var connectStrLocalEN = builder.Configuration["LocalDbConnectionString"]; try to  get connecton string from environment variable
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectStr));


// *************** read AppSettings secation  ************************************
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppConfigAppSettings>(appSettingsSection);

var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<AppConfigJwtSettings>(jwtSettingsSection);

var reportSettingsSection = builder.Configuration.GetSection("ReportSettings");
builder.Services.Configure<AppConfigReportSettings>(reportSettingsSection);

// *******************************************************************************


builder.Services.AddScoped<IAppServices, AppServices>();

//*************** Add API Version control *********************************************

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
//********************************************************************************

builder.Services.AddControllers(option =>
{
    option.CacheProfiles.Add("Default30", new CacheProfile() { Duration = 30 });
    option.CacheProfiles.Add("Default60", new CacheProfile() { Duration = 60 });
    option.CacheProfiles.Add("Default120", new CacheProfile() { Duration = 120 });
});
    //.AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();


// ***************Add support the XML file ******************************************
// application/XML in Request header
//builder.Services.AddControllers( options =>
//{
//    options.ReturnHttpNotAcceptable = true;
//}).AddXmlDataContractSerializerFormatters();
// ****************************************************************************

 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "ASM  API V1",
        Description = "API (V1) to manage Application Security Manager",
        TermsOfService = new Uri("https://example.api.tcdsb.org/terms"),
        Contact = new OpenApiContact
        {
            Name = "Frank Mi",
            Url = new Uri("https://api.tcdsb.org")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2.0",
        Title = "ASM API V2",
        Description = "API (V2) to manage Application Security Manager",
        TermsOfService = new Uri("https://example.api.tcdsb.org/terms"),
        Contact = new OpenApiContact
        {
            Name = "Frank Mi",
            Url = new Uri("https://dotnetmastery.com")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});


// ***************Support File system *******************************************************
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
// ************** End Support File system********************************** 

// *********************Config Authentication with JWT token **************************************

var key = builder.Configuration.GetValue<string>("JwtSettings:JWTSecret");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x => {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


// ************************************************************************
/* Config a third part log system use custom log system such as Serilog 
    1. This need installl some Serilog package in NuGet Package Manager
    2. using Serilog;
    3. Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
       .WriteTo.File("log/ApprTypeAPILogs.text", rollingInterval: RollingInterval.Day).CreateLogger();
    4. builder.Host.UseSerilog();
    5. inject it in the API constraction 
*/
/* Config a custom log system 
    1. create ILogging interface and excute class. inject it in the API constraction
    2. builder.Services.AddSingleton<ILogging, Logging>();
    3. public ApprTypeController(ILogging logger)
        { _logger = logger;  }
 */

builder.Services.AddSingleton<ILogging, Logging>();

// ************************************************

//************** add cross web site access ******************************************
   builder.Services.AddCors(); // uncoment this when front end application access api 
//*****************************************************
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ASM-API.V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "ASM-API.V2");
    });

}

app.UseHttpsRedirection();

app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); //cross web site access 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
