using Microsoft.EntityFrameworkCore;

namespace ProjetDevOps.Repositories;

public class WeatherRepository(WeatherDbContext dbContext)
{
    public async Task<IEnumerable<WeatherForecast>> GetAllWeather()
    {
        var weathers = await dbContext.Weathers.ToListAsync();
        return weathers.OrderBy(w => w.Date).ToList();
    }
}
