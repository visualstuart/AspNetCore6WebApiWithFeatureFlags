using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// add Azure App Configuration and use feature flags
builder.Host
    .ConfigureAppConfiguration(config =>
        {
            var settings = config.Build();
            var connection = settings.GetConnectionString("AppConfig");
            config.AddAzureAppConfiguration(options =>
                options.Connect(connection).UseFeatureFlags());
        });

builder.Services.AddControllers();

// add feature management to the container
builder.Services.AddFeatureManagement();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
