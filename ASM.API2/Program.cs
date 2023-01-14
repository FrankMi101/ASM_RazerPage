using ASM.DataAccess;
using ASM.DataAccess.Repository;
using ASM.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var currentDB = builder.Configuration.GetConnectionString("CurrentDb");
var connectStr = builder.Configuration.GetConnectionString(currentDB);
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectStr));


// *************** read AppSettings secation  ************************************
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
// *******************************************************************************


builder.Services.AddScoped<IAppServices, AppServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//  *** This is for API test purpose. should comment out once API works *** 
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hello World! API Test");
//});

app.Run();
