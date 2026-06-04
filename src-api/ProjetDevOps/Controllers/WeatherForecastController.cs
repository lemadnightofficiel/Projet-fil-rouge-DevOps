using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ProjetDevOps.Repositories;

namespace ProjetDevOps.Controllers;

public static class WeatherForecastController
{
    public static async Task<IResult> GetWeather(
        [FromServices] WeatherRepository repository)
    {
        var weathers = await repository.GetAllWeather();

        return Results.Ok(weathers);
    }

    public static IEndpointRouteBuilder AddWeatherEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/weathers", GetWeather)
            .Produces<WeatherForecast>((int)HttpStatusCode.OK, MediaTypeNames.Application.Json)
            .WithName(nameof(GetWeather));

        builder.MapPost("/api/weather", InsertWeather)
            .WithName(nameof(InsertWeather));

        return builder;
    }

    public static async Task<IResult> InsertWeather(
        [FromBody] WeatherForecast weather,
        [FromServices] WeatherRepository repository)
    {
        await repository.InsertAllWeather(weather);

        return Results.Created();
    }
}
