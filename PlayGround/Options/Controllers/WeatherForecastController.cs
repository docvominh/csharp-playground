using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Options.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(
    IOptions<AppSettings> appSettings, // Get config only when app start
    IOptionsSnapshot<AppSettings> appSnapshotSettings, // Get config every request, only work with scoped services. Ex: Controller
    IOptionsMonitor<AppSettings> appMonitoringSettings, // Rebuild config when json config changed
    ILogger<WeatherForecastController> logger
) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5)
            .Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    AppName1 = appSettings.Value.AppName,
                    AppName2 = appSnapshotSettings.Value.AppName,
                    AppName3 = appMonitoringSettings.CurrentValue.AppName
                }
            )
            .ToArray();
    }
}