using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
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

    private readonly IConfigurationRefresher configurationRefresher;
    private readonly IFeatureManager featureManager;
    private readonly ILogger<WeatherForecastController> logger;

    public WeatherForecastController(
        IConfigurationRefresherProvider refresherProvider,
        IFeatureManagerSnapshot featureManager,
        ILogger<WeatherForecastController> logger)
    {
        configurationRefresher = refresherProvider.Refreshers.First();
        this.featureManager = featureManager;
        this.logger = logger;
    }   

    [FeatureGate(MyFeatureFlags.Beta)]
    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        using (var scope = logger.BeginScope("Fun with feature flags"))
        {
            _ = await configurationRefresher.TryRefreshAsync();
            bool isGamma = await featureManager.IsEnabledAsync(nameof(MyFeatureFlags.Gamma));

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = isGamma
                    ? "Gamma"
                    : Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
