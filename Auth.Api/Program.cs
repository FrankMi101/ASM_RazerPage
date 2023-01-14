
using ASM.Models; 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using ASM.DataAccess;
using ASM.DataAccess.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Auth.Api.Models;
using Authen;
using ASM.DataAccess.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DB connection
var currentDB = builder.Configuration.GetConnectionString("CurrentDb");
var connectStr = builder.Configuration.GetConnectionString(currentDB);
 builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectStr));


// *************** read AppSettings secation  ************************************
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppConfigAppSettings>(appSettingsSection);

var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<AppConfigJwtSettings>(jwtSettingsSection);

// *******************************************************************************


builder.Services.AddScoped<IAppServices, AppServices>();
builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddSingleton<IRequestContext, RequestContext>();

builder.Services.AddControllers();


// ***************Add support the XML file ******************************************
// application/XML in Request header
//builder.Services.AddControllers( options =>
//{
//    options.ReturnHttpNotAcceptable = true;
//}).AddXmlDataContractSerializerFormatters();
// ****************************************************************************

builder.Services.AddSingleton<ILogging, Logging>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//************************Add JWT bearer Authentication ***************************** 
// Add Authentication in Swaggergen APi page
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
         Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Standard Authorization header using the Bearer scheme(\"bearer {token}\")",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type =  Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey //Microsoft.OpenApi.Models.SecuritySchemeType.Http 
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var SecretKey = builder.Configuration["JwtSettings:JWTSecret"];

// builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey))
    };
});



// ***********************************************************************************

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
