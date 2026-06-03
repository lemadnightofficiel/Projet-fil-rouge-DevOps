using Microsoft.EntityFrameworkCore;

namespace ProjetDevOps.Repositories;

public class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> Weathers => Set<WeatherForecast>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
