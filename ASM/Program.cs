using ASM.DataAccess;
using ASM.DataAccess.Repository;
using ASM.DataAccess.Services;
using ASM.Models;
using Authen;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
//using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();


// Add DB connection
var currentDB = builder.Configuration.GetConnectionString("CurrentDb");
var connectStr = builder.Configuration.GetConnectionString(currentDB);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectStr));

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

// *************** Add API Call Service  ************************************
 builder.Services.AddHttpClient<IAppsModelServiceAPICall, AppsModelServiceAPICall>();
 builder.Services.AddScoped<IAppsModelServiceAPICall, AppsModelServiceAPICall>();
// *******************************************************************************

// *************** read AppSettings secation  ************************************
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppConfigAppSettings>(appSettingsSection);
 

var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<AppConfigJwtSettings>(jwtSettingsSection);

// *******************************************************************************



builder.Services.AddScoped<IAppServices, AppServices>();
builder.Services.AddScoped<IAuthService, AuthService>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme )
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => builder.Configuration.Bind("JwtSettings", options))
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => builder.Configuration.Bind("CookieSettings", options));

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => builder.Configuration.Bind("JwtSettings", options))
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => builder.Configuration.Bind("CookieSettings", options));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
       {
           options.Cookie.Name = ASM.AppConstant.AuthCookName;   
           options.LoginPath = "/Account/Login";
           options.AccessDeniedPath = "/Account/AccessDenied";
           options.ExpireTimeSpan =  TimeSpan.FromMinutes(5);
           //options.SlidingExpiration = true;
       });
//builder.Services.AddAuthentication("ASMCookieAuthentication")
//        .AddCookie("ASMCookieAuthentication", options =>
//        {
//            options.Cookie.Name = "ASMCookieAuthentication";
//            options.LoginPath = "/Account/Login";
//            options.AccessDeniedPath = "/Account/Forbidden";
//            //options.SlidingExpiration = true;
//        });
//.AddJwtBearer(options =>
//{
//    options.Audience = "http://localhost:5001/";
//    options.Authority = "http://localhost:5000/";
//});
//********************************************************************************** 


builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});


// ************* config applicaiton session ************************************
builder.Services.AddSession( options =>
{
    options.Cookie.HttpOnly = true;
    options.IdleTimeout= TimeSpan.FromHours(8);
    options.Cookie.IsEssential= true;
});

// *****************************************************************************


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllers();

app.Run();
 