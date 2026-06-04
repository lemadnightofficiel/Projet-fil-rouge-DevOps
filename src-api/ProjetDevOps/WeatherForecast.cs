using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetDevOps;

public class WeatherForecast
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid IdWeather { get; set; } = Guid.NewGuid();

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public string Town { get; set; }
    
    public string PostalCode { get; set; }

    public string? Summary { get; set; }
}
