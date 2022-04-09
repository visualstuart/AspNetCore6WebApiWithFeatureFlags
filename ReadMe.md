Adapting [Quickstart: Add feature flags to an ASP.NET Core app | Microsoft Docs](https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-feature-flag-aspnet-core?tabs=core6x%2Ccore5x#add-secret-manager)
to an ASP.NET Core 6 Web API project.

````
dotnet new webapi
dotnet user-secrets init
dotnet add package Microsoft.Azure.AppConfiguration.AspNetCore
dotnet add package Microsoft.FeatureManagement.AspNetCore
dotnet user-secrets set ConnectionStrings:AppConfig "your_connection_string"
````

See _Program.cs_ and _Controllers/WeatherForecastController.cs_ for 
configuring and using feature flags from Azure App Config.