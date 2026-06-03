using System.Data;
using ProjetDevOps.Controllers;
using ProjetDevOps.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
if (string.IsNullOrEmpty(connectionString))
{
    throw new NoNullAllowedException(nameof(connectionString));
}

builder.Services.AddSingleton<WeatherRepository>();

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

builder.Services.AddOpenApi();

var app = builder.Build();

app.AddWeatherEndpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
