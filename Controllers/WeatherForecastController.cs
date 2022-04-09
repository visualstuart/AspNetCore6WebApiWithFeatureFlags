using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace AspNetCore6WebApiWithFeatureFlags.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IFeatureManager featureManager;
    private readonly ILogger<WeatherForecastController> logger;

    public WeatherForecastController(
        IFeatureManagerSnapshot featureManager,
        ILogger<WeatherForecastController> logger)
    {
        this.featureManager = featureManager;
        this.logger = logger;
    }

    [FeatureGate(MyFeatureFlags.Beta)]
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
