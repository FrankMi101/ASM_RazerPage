
using ASMWeb.DataAccess.DbInitializer;
using ASMWeb.DataAccess;
using ASMWeb.DataAccess.Repository;
using ASMWeb.Utility;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Authen;
using ASMWeb.Models;
using ASMWeb.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var currentDB = builder.Configuration.GetConnectionString("CurrentDbStr");
var connectStr = builder.Configuration.GetConnectionString(currentDB);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectStr));


builder.Services.AddScoped<IDbInitializer, DbInitializer>();

// *************** read AppSettings secation  ************************************
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
// *******************************************************************************



//**************** using default *****************************************************
//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
//********************************************************************



builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddScoped<IAppActionsCatalog, AppActionsCatalog>();
//builder.Services.AddScoped<IAppsRepository, AppsRepository>();
// builder.Services.AddScoped<IAppsModelsRepository, AppsModelsRepository>();

// ************* try to add user to claim identity does not work *************************************
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => builder.Configuration.Bind("JwtSettings", options))
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => builder.Configuration.Bind("CookieSettings", options));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie("ASMCookieAuthentication", options =>
        {
            options.Cookie.Name = "ASMCookieAuthentication";
             options.LoginPath = "/Account/Login2/";
             options.AccessDeniedPath = "/Account/Forbidden/";
           //options.SlidingExpiration = true;
        });
        //.AddJwtBearer(options =>
        //{
        //    options.Audience = "http://localhost:5001/";
        //    options.Authority = "http://localhost:5000/";
        //});
//********************************************************************************** 


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login2";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});


//builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppUserClaimsPrincipalFactory>();



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

 SeedDatabase();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();



void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}