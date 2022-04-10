using Microsoft.FeatureManagement;

/// <summary>
/// The default cache expiration interval is 30 seconds, which obtained by omitting the parameter
/// to AddAzureAppConfiguration.UseFeatureFlags. 
/// Setting it artificially low for development.
/// </summary>
const double FeatureFlagCacheExpirationIntervalInSeconds = 1;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Add Azure App Configuration and use feature flags and not load other configuration values.
/// Sets the feature flag cache expiration interval instead of using default value.
/// </summary>
_ = builder.Host
    .ConfigureAppConfiguration(config => config
        .AddAzureAppConfiguration(options => options
            .Connect(config.Build().GetConnectionString("AppConfig"))
            // "_" is a nonexistent dummy configuration key in order to only load feature flags
            .Select("_")
            .UseFeatureFlags(options => options
                .CacheExpirationInterval = 
                    TimeSpan.FromSeconds(FeatureFlagCacheExpirationIntervalInSeconds))));

_ = builder.Services.AddControllers();

_ = builder.Services
    .AddAzureAppConfiguration()
    .AddFeatureManagement();    // add feature management to the container

_ = builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app
        .UseSwagger()
        .UseSwaggerUI();
}

_ = app
    .UseHttpsRedirection()
    .UseAuthorization();
_ = app.MapControllers();

app.Run();