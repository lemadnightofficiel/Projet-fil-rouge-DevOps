using ProjetDevOps.Controllers;
using ProjetDevOps.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
//if (string.IsNullOrEmpty(connectionString))
//{
//    throw new NoNullAllowedException(nameof(connectionString));
//}

builder.Services.AddControllers();



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<WeatherRepository>();

var app = builder.Build();

app.AddWeatherEndpoint();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
