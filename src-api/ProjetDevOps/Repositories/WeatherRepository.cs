using Microsoft.EntityFrameworkCore;

namespace ProjetDevOps.Repositories;

public class WeatherRepository(WeatherDbContext dbContext)
{
    public async Task<IEnumerable<WeatherForecast>> GetAllWeather()
    {
        var weathers = await dbContext.WeatherForecasts.ToListAsync();
        return weathers.OrderBy(w => w.Date).ToList();
    }

    public async Task InsertAllWeather(WeatherForecast weather)
    {
        var value = await dbContext.WeatherForecasts.AddAsync(weather);
        await dbContext.SaveChangesAsync();
    }
}
