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

        return builder;
    }
}
