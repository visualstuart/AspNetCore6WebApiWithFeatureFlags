using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// add Azure App Configuration and use feature flags
builder.Host
    .ConfigureAppConfiguration(config => config
        .AddAzureAppConfiguration(options => options
            .Connect(config.Build().GetConnectionString("AppConfig"))
            .UseFeatureFlags()));

builder.Services.AddControllers();

builder.Services.AddFeatureManagement();    // add feature management to the container

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseAuthorization();
app.MapControllers();

app.Run();