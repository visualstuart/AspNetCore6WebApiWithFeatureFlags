# Objectives

This sample adapts [Quickstart: Add feature flags to an ASP.NET Core app | Microsoft Docs](https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-feature-flag-aspnet-core?tabs=core6x%2Ccore5x#add-secret-manager)
to an ASP.NET Core 6 Web API project, and adds some additional feature flag capabilities.

This sample demonstrates:

* [[FeatureGate]](https://docs.microsoft.com/en-us/dotnet/api/microsoft.featuremanagement.mvc.featuregateattribute)
attribute to flag a controller action (can also be applied to a controller); 
see the `WeatherForecastController.Get` method.
* Dependency injection of 
[IFeatureManagerSnapshot](https://docs.microsoft.com/en-us/dotnet/api/microsoft.featuremanagement.ifeaturemanagersnapshot)
to access feature flag values, and 
[IConfigurationRefresherProvider](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.azureappconfiguration.iconfigurationrefresherprovider)
used to reload feature flag values; see the `WeatherForecastController` constructor.
* Using the injected dependencies to try refreshing (reloading) feature flag values, and using feature flag values;
see the `WeatherForecastController.Get` method.

Additionally, the sample:

* Ensures that only feature flags, and no other configuration values, are loaded; 
see the invocation of the `AzureAppConfigurationOptions.Select` method use in Program.cs using the "_" parameter.
* Sets the feature flag cache expiration interval to an artificially low value (1 second) to facilitate local development.

# Requirements

This sample requires:

* An Azure App Configuration instance (see any of the Microsoft Quickstarts to set up an Azure App Configuration instance and clean it up when done)
   * With the connection string configured in local User Secrets (see steps used below)
   * and two feature flags named `Beta` and `Gamma` with no filtering.

The following steps were used to create project and set up the connection string to
Azure App Config in local User Secrets.

````
dotnet new webapi
dotnet add package Microsoft.Azure.AppConfiguration.AspNetCore
dotnet add package Microsoft.FeatureManagement.AspNetCore

dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:AppConfig "your_connection_string"
````

See _Program.cs_ and _Controllers/WeatherForecastController.cs_ for 
configuring and using feature flags from Azure App Config.

# References

* [Quickstart: Create a .NET Core app with App Configuration | Microsoft Docs](https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-dotnet-core-app)
  * The main starting point for this sample.
This sample uses a `webapi` project instead of a `mvc` application template, 
however that doesn't affect the use of feature flags in the example.
* [Quickstart: Add feature flags to an Azure Functions app | Microsoft Docs](https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-feature-flag-azure-functions-csharp)
  * Illustrates suppressing loading other configuration and how to refresh (reload) feature flags.
* [WebDemoNet6](https://github.com/Azure/AppConfiguration/tree/main/examples/DotNetCore/WebDemoNet6)
in Azure / AppConfiguration / examples / DotNetCore on GitHub.
* [What's new in ASP.NET Core 6.0 | Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0),
and particularly the "Web app template improvements" section.
* [What's new in ASP.NET Core 5.0 | Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-5.0),
and particularly the "Web API" section. 